<?php

class ModeratorsModel 
{
    public static function getModeratorById($moderatorId)
    {
        $sql = "
            SELECT `login` 
            FROM `moderators`
            WHERE `idModerator` = ?
            LIMIT 1
        ";
        return Database::query($sql, array($moderatorId));
    }
}

?>
