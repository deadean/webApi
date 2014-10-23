<?php

class ClientsModel
{
    public static function getClients()
    {
        $sql = "
            SELECT u.id, u.name, u.surname, u.email, u.phone
            FROM users AS u
        ";
        return Database::query($sql);
    }
    
    public static function getClient($clientId)
    {
        $sql = "
            SELECT u.id, u.name, u.surname, u.email, u.phone
            FROM users AS u
            WHERE u.id = ?
            LIMIT 1
        ";
        return Database::query($sql, array($clientId));
    }
};

?>
