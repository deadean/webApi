<?php

class Database {
    private static $operation;
    private static $sql = '';
    private static $limit;
    private static $error_message = '';
    private static $result;
    private static $rows_count = 0;

    public static function init() {
        mysql_connect(Config::$db_host, Config::$db_user, Config::$db_pass);
        mysql_select_db(Config::$db_name);
        mysql_set_charset("utf8");
    }

    public static function getError() {
        return self::$error_message;
    }

    public static function query($sql, $values = array()) {
        self::$limit = -1;
        self::$sql = $sql;
        if (!self::formSql($values)) {
            return false;
        }
        return self::makeQuery();
    }

    public static function getRows() {
        return self::$rows_count;
    }
    
    public static function getLastInsertId() {
        return mysql_insert_id();
    }

    private static function formSql($values) {
        // Замена шаблона на значения
        if (!empty($values)) {
            foreach ($values as $value) {
                // Защита значений
                $value = self::secureInfo($value);
                // Замена шаблона на значение
                $n_sql = preg_replace("/[?]{1}/", $value, self::$sql, 1);
                // Произошла замена, но строка не изменилась
                if ($n_sql == self::$sql) {
                    self::$error_message = 'Patterns count are less than values count.';
                    return false;
                }
                self::$sql = $n_sql;
            }
        }

        if (substr_count(self::$sql, "?")) {
            self::$error_message = 'Values count are less than patterns count.';
            return false;
        }

        self::setSqlOperation(self::$sql);
        self::setLimit(self::$sql);
        return true;
    }

    private static function secureInfo($value) {
        if (get_magic_quotes_gpc()) {
            $value = stripslashes($value);
        }
        if (!is_numeric($value)) {
            $value = "'" . mysql_real_escape_string($value) . "'";
        }
        return $value;
    }

    private static function setSqlOperation($sql) {
        $sql = strtolower(trim($sql));
        preg_match("/(select)|(insert)|(delete)|(update)/", $sql, $operation);
        self::$operation = $operation[0];
    }

    private static function setLimit($sql) {
        $sql = explode(" ", $sql);
        foreach ($sql as $key => $part) {
            if (strtolower($part) == 'limit') {
                self::$limit = $sql[$key + 1];
                return;
            }
        }
    }

    private static function makeQuery() {
        $result = mysql_query(self::$sql);
        if (!$result) {
            self::$error_message = 'Database error: ' . mysql_error();
            return false;
        }

        self::$rows_count = mysql_affected_rows();

        if (self::$operation != 'select') {
            return $result;
        } else {
            $info = array();
            while ($row = mysql_fetch_assoc($result)) {
                $info[] = $row;
            }
            if (!empty($info)) {
                if (self::$limit == 1) {
                    return $info[0];
                }
                return $info;
            }
            return false;
        }
    }
}

?>
