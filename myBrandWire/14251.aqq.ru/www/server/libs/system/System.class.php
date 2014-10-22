<?php
class System {
    private static $stage = 'no';

    private static $controller;
    private static $function;
    private static $params;

    private static $result;

    public static function run() {
        // Инициализация системы
        self::init();
        // Обработка запроса
        self::parseRequest();
        // Инициализация контроллера
        self::initController();
        // Подключение моделей, которые использует контроллер
        self::initModels();
        // Подключение компонентов, которые использует контроллер
        self::initComponents();
        // Проверка существования функции
        self::checkFunction();
        // Проверка параметров функции
        self::checkParams();
        // Выполнение функции
        self::processRequest();
        // Формируем ответ
        self::formResult();
    }

    private static function init() {
        // Подключение конфиг файла
        self::$stage = "Init config";
        include("Config.class.php");

        // Установка функции для обработки ошибок
        if(!Config::$debug)
            set_error_handler("System::ErrorHandler");

        // Подключение и инициализация базы данных
        self::$stage = "Init database";
        include("Database.class.php");
        Database::init();
    }

    private static function parseRequest() {
        self::$stage = "Parse request";
		
        $url_info = explode("/", $_SERVER['REDIRECT_URL']);

        // Проверка класса апи
        if($url_info[1] != 'server')
            self::ProcessError('Wrong api');
        
        // Проверка имени класса контроллера
        if(empty($url_info[2]))
            self::processError('Empty class name');
        
        self::$controller = $url_info[2];

        // Проверка имени исполняемой функции класса
        if(empty($url_info[3]))
            self::processError('Empty function name');
        
        self::$function = $url_info[3];

        
        // Сохранение параметров
        if (!empty($_GET))
        {
            self::$params = $_GET;
        }
        else 
        {
            self::$params = $_POST;
        }
        
        if (!empty($_FILES))
        {
            self::$params = array_merge(self::$params, $_FILES);
        }
    }

    private static function initController() {
        self::$stage = "Init controller " . self::$controller;
        // Подключение базового контроллера
        include('libs/system/Controller.class.php');
        // Подключение исполняемого контроллера
        include("libs/controllers/" . self::$controller . "Controller.class.php");
        call_user_func(array(self::$controller.'Controller', 'init'));
    }

    private static function initModels() {
        self::$stage = "Init class models";
        $models = call_user_func(array(self::$controller.'Controller', 'GetModels'));
        if(!empty($models)) {
            foreach($models as $model) {
                self::$stage = "Init model " . $model;
                include('libs/models/' . $model . 'Model.class.php');
            }
        }
    }

    private static function initComponents() {
        self::$stage = "Init class components";
        $components = call_user_func(array(self::$controller.'Controller', 'GetComponents'));
        if(!empty($components)) {
            foreach($components as $component) {
                self::$stage = "Init model " . $component;
                include('libs/components/' . $component . 'Component.class.php');
            }
        }
    }

    private static function checkFunction() {
        // Проверка существует ли функция
        self::$stage = 'Check function';
        if(!method_exists(self::$controller.'Controller', self::$function)) {
            self::processError('Function does not exists');
        }
    }

    private static function checkParams() {
        self::$stage = 'Check params';
        if(!call_user_func(array(self::$controller.'Controller', 'ValidateAndSetParams'), self::$params, self::$function))
            self::processError('Some params were not found');
    }

    private static function processRequest() {
        // Выполенение запроса
        self::$stage = 'Process request';
        self::$result = call_user_func(array(self::$controller.'Controller', self::$function), self::$params);
    }

    private static function formResult() {
        $response = array(
            'api' => 'server',
            'controller' => self::$controller,
            'method' => self::$function
        );

        if(self::$stage == 'Process request') {
            if(call_user_func(array(self::$controller.'Controller', "isRequestOk"))) {
                $response['status'] = 'ok';
                $response['info'] = self::$result;
            }
            else {
                $response['status'] = 'error';
                $response['info'] = array(
                    'stage' => self::$stage,
                    'errorDescription' => self::$result
                );
            }
        }
        else {
            $response['status'] = 'error';
            $response['info'] = self::$result;
        }

        if(Config::$response_type == 'array')
            print_r($response);
        elseif(Config::$response_type == 'json')
            echo json_encode($response);
        exit;
    }

    public static function errorHandler() {
        self::processError();
    }

    private static function processError($message = '') {
        self::$result = array(
            "stage" => self::$stage,
            "errorDescription" => $message
        );
        self::formResult();
    }
}
?>
