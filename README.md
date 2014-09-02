AuthishModule
=============

Authish is a quick-and-dirty login-wrapper to your .NET project. It is mainly intended
for making development and demo environments unavailable to anyone on the internet.
It can probably be hacked quite easily, so do not trust it for anything really sensitive.

Setup is done by:
 * Adding SimpleAuthishModule to web.config (system.webServer > Modules)
 * Setting "AuthishPassword" in appSettings to your password of choice
 
We do not auto-update web.config with the above because:
 * You typically don't want this in your dev-setup. We add it via web.config transformations in our deploy server (Octopus!).
 * You may prefer (as we do) to have appSettings in a separate file referenced from web.config
 * We prefer not to auto-set a password, as that would make it even less secure if you don't change it ;-)
