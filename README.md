AuthishModule
=============

Authish is a quick-and-dirty login-wrapper to your .NET project. It is mainly intended for making development and demo environments
unavailable to everyone on the internet, and can probably be hacked quite easily, so do not trust it for anything really sensitive.

Setup is done by:
 * Adding SimpleAuthishModule to system.webServer > Modules
 * Setting "AuthisPassword" in appSettings to your password of choice
 
