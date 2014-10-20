<?php
namespace Cookies;

use php\Implementations\Cookies\CCookie;
use php\Interfaces\Cookies\CookieBase;
use php\Interfaces\Cookies\ICookieService;

class CCookiesService implements  ICookieService {

    function SetCookiesForUser(CookieBase $cookieInformation)
    {
        setcookie(\csConstants::$csUSER
            ,$cookieInformation->login
            ,time()+604800,'/');
        setcookie($cookieInformation->login
            ,$cookieInformation::GetPasswordHash($cookieInformation->password)
            ,time()+604800,'/');
    }

    function GetCookiesForUser()
    {
        if(isset($_COOKIE[\csConstants::$csUSER])){
            $result = new CCookie();
            $result->login = $_COOKIE[\csConstants::$csUSER];
            $result->password = $_COOKIE[$_COOKIE[\csConstants::$csUSER]];
        }
        return null;
    }

    function IsUserCookiesExist(){
        return isset($_COOKIE[\csConstants::$csUSER]);
    }
}