# AuthishModule

Authish is a quick-and-dirty login-wrapper to your .NET project. It is mainly intended
for making development and demo environments unavailable to anyone on the internet.
It can probably be hacked quite easily, so do not trust it for anything really sensitive.

# Setup

- Install AuthishModule from [nuget](https://www.nuget.org/packages/AuthishModule/)

```
Install-Package AuthishModule
```

- Add SimpleAuthishModule to web.config (system.webServer > Modules)

```
<system.webServer>
  <modules>
    <add name="AuthishModule" type="AuthishModule.BlockingModule, AuthishModule,Version=1.7.0.0" preCondition="managedHandler" />
  </modules>
</system.webServer>
```

- Set "AuthishPassword" in appSettings to your password of choice

```
<add key="AuthishPassword" value="your-password" />
```

Normally, the user will be challenged with a password prompt. But you can also authenticate
by passing the password in a request header named "Authish". This can easily be added to,
e.g., Chrome by using [ModHeader](https://chrome.google.com/webstore/detail/modheader/idgpnmonknjnojddfkpgkljpfnnfcklj).

![image](https://cloud.githubusercontent.com/assets/77299/4678864/588bbebc-55fb-11e4-94ab-ef000c4055b1.png)

But header authentication is mostly added to enable load testing (via tools like [Netling](https://github.com/hallatore/Netling))
or automated verification tools to bypass authentication without having to support cookies.

We do not auto-update web.config with the above because:

- You typically don't want this in your dev-setup. We add it via web.config transformations in our deploy server (Octopus!).
- You may prefer (as we do) to have appSettings in a separate file referenced from web.config
- We prefer not to auto-set a password, as that would make it even less secure if you don't change it

## Whitelist paths

Set "AuthishWhitelistedPaths" in appSettings to specifiy whitelisted paths. The value is a comma separated string.

```
<add key="AuthishWhitelistedPaths" value="path1,path2,path3" />
```

## Whitelist start of paths

Set "AuthishWhitelistedStartOfPaths" in appSettings to specifiy whitelisted paths. The value is a comma separated string.

```
<add key="AuthishWhitelistedStartOfPaths" value="startOfPath1,startOfPath2,startOfPath3" />
```

This is for instance useful for static files in a common folder.
