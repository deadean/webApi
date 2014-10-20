<?php
/**
 * Created by PhpStorm.
 * User: dean
 * Date: 30.06.14
 * Time: 19:08
 */

namespace php\Implementations\Cookies;
use php\Interfaces\Cookies\CookieBase;

class CCookie extends CookieBase {

    public function GetPasswordHash($password)
    {
        return md5($password);
    }
}