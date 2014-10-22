<?php   
    error_reporting(E_ALL);
    ini_set('display_errors', 1);
    
    header("Content-Type: text/html; charset=utf-8");
    header("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");

    include('libs/system/System.class.php');
    System::run();
?>