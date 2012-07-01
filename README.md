## Long SMS

### [See it live!](https://secure.elfeon.com/sms)

Goal: provide a simple website where Google Voice users can enter a text message of any length and send it in 160-character chunks.

I wrote this when I discovered that Google Voice doesn't play well with messages over 320 characters (twice the maximum size of a single SMS message). Up to 320 characters, Google Voice automatically splits messages into 160-character chunks and sends them, but if you go beyond that amount, the rest is discarded.

This app seeks to solve that by splitting a submitted message into 160-character chunks before it reaches Google's servers, thus avoiding the character limits Google enforces. In other words, if you need to send a long text message, it will probably be cut off if you send it directly through Google Voice, but this app will divide your message into chunks and send them individually.

### Technical details

I wrote this code very quickly, so there is little documentation. But the project's functionality is fairly simple, so hopefully it is self-explanatory. A standard ASP.NET MVC infrastructure is used, and respones are cached and gzip-ed.

Platform:
 * .NET 4
 * ASP.NET MVC 3
 * [Twitter Bootstrap](http://twitter.github.com/bootstrap)
 * [SharpGoogleVoice](http://bitbucket.org/jitbit/sharpgooglevoice)
 * jQuery
 * [RiaLibrary.Web](http://maproutes.codeplex.com/)

See the `LICENSE` file for license information.