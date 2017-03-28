# SecuredApi

This is a proof of concept in order to implement OAuth2's implicit grant flow within a spa js app using IdentityServer3.

*JS libs*

Use bower to resolve all (client side) js libraries.

*It works on my machine*

It will run out of the box using the following setup:
- IdentityServer: https://secured.local:449/identityserver.
- SpaClient: https://secured.local:449/spaclient.
- ResourceServer: https://secured.local:449/resourceserver.
- WS federation server: https://localhost:44300.

Otherwise choose whatever hosts you like and adjust the app settings accordingly.

Both the identity server and the ws federation server do require ssl, the others don't.
The federation server project is configured to run in iis express but you could also run it in iis if you want.
Just make sure the domain url remains localhost because it uses windows authentication and therefore a trusted domain like localhost is required.
