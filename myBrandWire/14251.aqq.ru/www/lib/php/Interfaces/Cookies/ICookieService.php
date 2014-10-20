<?php
/**
 * Created by PhpStorm.
 * User: dean
 * Date: 30.06.14
 * Time: 18:53
 */

namespace php\Interfaces\Cookies;


interface ICookieService {
    function SetCookiesForUser(CookieBase $cookieInformation);
    function IsUserCookiesExist();
    function GetCookiesForUser();
} 