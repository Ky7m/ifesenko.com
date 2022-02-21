using System;
using Microsoft.AspNetCore.Mvc;

namespace PersonalWebApp.Controllers;

public class AntiBotController: Controller
{
    private static readonly string[] TenHoursOfFun =
    {
        "https://www.youtube.com/watch?v=wbby9coDRCk",
        "https://www.youtube.com/watch?v=nb2evY0kmpQ",
        "https://www.youtube.com/watch?v=eh7lp9umG2I",
        "https://www.youtube.com/watch?v=z9Uz1icjwrM",
        "https://www.youtube.com/watch?v=Sagg08DrO5U",
        "https://www.youtube.com/watch?v=A3YmHZ9HMPs",
        "https://www.youtube.com/watch?v=jI-kpVh6e1U",
        "https://www.youtube.com/watch?v=jScuYd3_xdQ",
        "https://www.youtube.com/watch?v=S5PvBzDlZGs",
        "https://www.youtube.com/watch?v=9UZbGgXvCCA",
        "https://www.youtube.com/watch?v=O-dNDXUt1fg",
        "https://www.youtube.com/watch?v=MJ5JEhDy8nE",
        "https://www.youtube.com/watch?v=VnnWp_akOrE",
        "https://www.youtube.com/watch?v=jwGfwbsF4c4",
        "https://www.youtube.com/watch?v=8ZcmTl_1ER8",
        "https://www.youtube.com/watch?v=gLmcGkvJ-e0",
        "https://www.youtube.com/watch?v=hGlyFc79BUE",
        "https://www.youtube.com/watch?v=KMFOVSWn0mI",
        "https://www.youtube.com/watch?v=clU0Sh9ngmY",
        "https://www.youtube.com/watch?v=sCNrK-n68CM"
    };

    private readonly Random _random = new();

    [Route("admin.php")]
    [Route("admin/login.php")]
    [Route("administrator/index.php")]
    [Route("ajaxproxy/proxy.php")]
    [Route("bitrix/admin/index.php")]
    [Route("index.php")]
    [Route("magmi/web/magmi.php")]
    [Route("wp-admin/admin-ajax.php")]
    [Route("wp-admin/includes/themes.php")]
    [Route("wp-admin/options-link.php")]
    [Route("wp-admin/post-new.php")]
    [Route("wp-login.php")]
    [Route("xmlrpc.php")]
    public ActionResult No() => Redirect(TenHoursOfFun[_random.Next(0, TenHoursOfFun.Length)]);
}