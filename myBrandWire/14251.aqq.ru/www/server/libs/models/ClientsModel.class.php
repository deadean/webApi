<?php

class ClientsModel
{
    public static function getClients()
    {
        $sql = "
            SELECT u.id, u.name, u.surname, u.email, u.phone
            FROM users AS u
            WHERE is_delete = 0
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
    
    public static function updateClientInfo($clientId, $name, $surname, $phone, $email) 
    {
        $sql = "
            UPDATE users
            SET name = ?,
                surname = ?,
                email = ?,
                phone = ?
            WHERE id = ?
            LIMIT 1
        ";
        Database::query($sql, array($name, $surname, $email, $phone, $clientId));
        return 1;
    }
    
    public static function removeClient($clientId)
    {
        $sql = "
            UPDATE users
            SET is_delete = 1
            WHERE id = ?
            LIMIT 1
        ";
        Database::query($sql, array($clientId));
        return Database::getRows();
    }
};

?>
