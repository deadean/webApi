<?php

class Controller {
    protected static $models = array();
    protected static $components = array();
    protected static $methods_params = array();
    protected static $data = array();

    protected static $is_request_ok = false;

    public static function validateAndSetParams($params, $function) {
        if(isset(self::$methods_params[$function]))  {
            $m_params = self::$methods_params[$function];
            if(!empty($m_params)) {
                foreach($m_params as $param) {
                    if(!isset($params[$param])) {
                        return false;
                    }
                }
            }
        }
        self::$data = $params;
        return true;
    }

    public static function getComponents() {
        return self::$components;
    }

    public static function getModels() {
        return self::$models;
    }

    public static function isRequestOk() {
        return self::$is_request_ok;
    }
}

?>
