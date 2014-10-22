<?php

class Config {
    // Информация для подключения к базе данных
    public static $db_host = 'localhost';

    ///*
    public static $db_user = 'root';
    public static $db_pass = '';
    public static $db_name = 'aqq14251_mybrandwire';
    //*/

    /*
    public static $db_user = 'mybrandw_root';
    public static $db_pass = 'mybrandwire';
    public static $db_name = 'mybrandw_mybrandwire';
    */

    // Включить вывод ошибок
    public static $debug = true;

    // Формат ответа: json, array
    public static $response_type = 'json';
    
    public static $moderatorId = 1;
}

?>
