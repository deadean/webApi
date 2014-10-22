<?php

class CompanyModel
{
    public static function getClientCompanies($clientId)
    {
        $sql = "
            SELECT id, name, region, community
            FROM company
            WHERE idUser = ?
        ";
        return Database::query($sql, array($clientId));
    }
};

?>
