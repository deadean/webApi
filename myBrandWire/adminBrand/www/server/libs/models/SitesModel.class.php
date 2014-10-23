<?php

class SitesModel
{
    public static function getSites()
    {
        $sql = "
            SELECT `s`.*, `sc`.name AS category_name 
            FROM `sites` AS s
            LEFT JOIN `sites_categories` AS sc
                ON s.idCategory = sc.idCategory
        ";
        return Database::query($sql);
    }
    
    public static function getSitesCategories()
    {
        $sql = "
            SELECT *
            FROM `sites_categories`
        ";
        return Database::query($sql);      
    }
    
    public static function getSite($id)
    {
        $sql = "
            SELECT `s`.*, `sc`.name AS category_name 
            FROM `sites` AS s
            LEFT JOIN `sites_categories` AS sc
                ON s.idCategory = sc.idCategory
            WHERE `idSite` = ?
            LIMIT 1
        ";
        return Database::query($sql, array($id));
    }

    
    public static function addNewSite($name, $url, $description, $categoryId)
    {
        $sql = "
            INSERT INTO `sites`
            SET `name` = ?,
                `url` = ?,
                `description` = ?,
                `idCategory` = ?
        ";
        Database::query($sql, array($name, $url, $description, $categoryId));
        return Database::getLastInsertId();
    }
    
    public static function updateSiteLogo($id, $logo)
    {
        $sql = "
            UPDATE `sites`
            SET `logo` = ?
            WHERE `idSite` = ?
            LIMIT 1;
        ";
        return Database::query($sql, array($logo, $id));
    }
    
    public static function deleteSite($id)
    {
        $sql = "
            DELETE FROM `sites`
            WHERE `idSite` = ?
            LIMIT 1
        ";
        Database::query($sql, array($id));
        return Database::getRows();
    }
    
    public static function setSiteCategory($siteId, $siteCategoryId)
    {
        $sql = "
            UPDATE `sites`
            SET `idCategory` = ?
            WHERE `idSite` = ?
            LIMIT 1
        ";
        Database::query($sql, array($siteCategoryId, $siteId));
        return Database::getRows();
    }
    
    public function bindSiteToNew($newId, $siteId)
    {
        $sql = "
            INSERT INTO `news_sites`
            SET `idNews` = ?,
                `idSite` = ?,
                `status` = 'notStarted'
        ";
        return Database::query($sql, array($newId, $siteId));
    }
    
    public function getNewSites($newId)
    {
        $sql = "
            SELECT s.idSite, s.name, s.url, s.logo, sc.name AS category_name, ns.idNews, ns.link, ns.status, ns.comment
            FROM `news_sites` AS ns
            LEFT JOIN `sites` AS s
                ON s.idSite = ns.idSite
            LEFT JOIN `sites_categories` AS sc
                ON s.idCategory = sc.idCategory
            WHERE ns.idNews = ?
        ";
        return Database::query($sql, array($newId));
    }
    
    public function getCategorySites($categoryId)
    {
        $sql = "
            SELECT idSite, name
            FROM sites
            WHERE idCategory = ?
        ";
        
        return Database::query($sql, array($categoryId));
    }
    
    public function deleteNewsSitesBySiteId($siteId) {
        $sql = "
            DELETE FROM `news_sites`
            WHERE `idSite` = ?
        ";
        Database::query($sql, array($siteId));
        return Database::getRows();
    }
    
    public static function updateNewSiteInfo($newId, $siteId, $link, $status, $comment) {
        $sql = "
            UPDATE `news_sites`
            SET `link` = ?,
                `status` = ?,
                `comment` = ?
            WHERE `idNews` = ?
                AND `idSite` = ?
            LIMIT 1
        ";
        Database::query($sql, array($link, $status, $comment, $newId, $siteId));
        return 1;
    }
}


?>
