using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiaLibrary.Web;
using gvtexter.Helpers;
using gvtexter.ViewModels;
using System.Globalization;
using System.Text;
using Jitbit.Utils;
using System.Threading;

namespace gvtexter.Controllers
{
	public class HomeController : CustomControllerBase
	{
		[Url("")]
		[OutputCache(Duration = 7200, VaryByParam = "None")]
		public ActionResult Index(string redir)
		{
			if(!string.IsNullOrWhiteSpace(redir))
			{
				ViewBag.redirFromSuccess = true;
			}
			return View(new SendViewModel());
		}

		[Url("send")]
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult SendText(SendViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View("Index", model);
			}

			try
			{
				ThreadPool.QueueUserWorkItem((obj) =>
					{
						SharpGoogleVoice gv = new SharpGoogleVoice(model.Username, model.Password);
						foreach (var i in SplitIntoChunks(model.Text.Trim(), 160))
						{
							gv.SendSMS(model.To, i); // send a chunk
							Thread.Sleep(500); // pause between chunks - Google Voice doesn't like to be hit with lots of near-simultaneous requests
						}
					});
				return RedirectToAction("Index", "Home", new { redir = "success" });
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("form", "There was a problem sending your text. Please try again.");
				ModelState.SetModelValue("form", new ValueProviderResult(null, null, CultureInfo.InvariantCulture));
				
				return View("Index", model);
			}
		}

		private IEnumerable<string> SplitIntoChunks(string text, int chunkSize)
		{
			int offset = 0;
			while (offset < text.Length)
			{
				int size = Math.Min(chunkSize, text.Length - offset);
				yield return text.Substring(offset, size);
				offset += size;
			}
		}
	}
}
