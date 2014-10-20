<?php
/**
 * Created by PhpStorm.
 * User: dean
 * Date: 30.06.14
 * Time: 18:54
 */

namespace php\Interfaces\Cookies;


abstract class CookieBase {
    public $login;
    public $password;

    public abstract function GetPasswordHash($password);
}