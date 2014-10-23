<?php

class SitesController extends Controller
{
    public static function init() 
    {
        self::$components = array('Sites');
        self::$models = array('Moderators', 'Sites');
        self::$methods_params = array(
            'addSite' => array('name', 'url', 'logo', 'category', 'description'),
            'getSites' => array(),
            'getSitesCategories' => array(),
            'editSite' => array('id', 'name', 'url', 'description', 'logo'),
            'removeSite' => array('id'),
            'getNewSites' => array('new_id'),
            'bindSitesCategoryToNew' => array('new_id', 'category_id'),
            'saveNewSiteInfo' => array('new_id', 'site_id', 'link', 'status', 'comment'),
            'makeReport' => array('new_id')
        );
    }    
    
    public static function getSites()
    {
        $sites = SitesModel::getSites();
        self::$is_request_ok = true;
        return $sites;
    }
    
    public static function getSitesCategories()
    {
        $categories = SitesModel::getSitesCategories();
        self::$is_request_ok = true;
        return $categories;
    }
    
    public static function addSite()
    {
        $name = self::$data['name'];
        $url = self::$data['url'];
        $logo = self::$data['logo'];
        $description = self::$data['description'];
        $categoryId = self::$data['category'];

        $id = SitesModel::addNewSite($name, $url, $description, $categoryId);
        if (!$id)
            return "Error 1";
        
        $fileName = $id . '.' . end(explode(".", $logo['name']));
        $uploadFile = '/home/hunter/work/adminka/client/images/logos/' . $fileName;
        if (!move_uploaded_file($logo['tmp_name'], $uploadFile))
            return "Error 2";
        
        if (!SitesModel::updateSiteLogo($id, $fileName))
            return "Error 3";
                
        self::$is_request_ok = true;
        return 1;
    }
    
    public static function removeSite()
    {
        $id = self::$data['id'];

        $siteInfo = SitesModel::getSite($id);
        if (!$siteInfo)
            return 'Site not found';
        
        if (!SitesModel::deleteSite($id))
            return "Error";
        
        if ($siteInfo['logo'] && !unlink('/home/hunter/work/adminka/client/images/logos/' . $siteInfo['logo']))
            return "Error 2";
        
        SitesModel::deleteNewsSitesBySiteId($id);
        
        self::$is_request_ok = true;
        return 1;
    }
    
    public static function setSiteCategory()
    {
        $siteId = self::$data['site_id'];
        $siteCategoryId = self::$data['site_category_id'];
        
        //TODO: Validation
        
        $result = SitesModel::setSiteCategory($siteId, $siteCategoryId);
        
        if (!$result)
            return "Error";
        
        self::$is_request_ok = true;
        return 1;
    }
    
    public static function getNewSites()
    {
        $newId = self::$data['new_id'];
        
        // TODO: Validation
        
        $sites = SitesModel::getNewSites($newId);
        
        self::$is_request_ok = true;
        return $sites;
    }
    
    
    
    public static function bindSitesCategoryToNew()
    {
        $newId = self::$data['new_id'];
        $siteCategoryId = self::$data['category_id'];
        
        $categorySites = SitesModel::getCategorySites($siteCategoryId);
        foreach ($categorySites as $site)
        {
            SitesModel::bindSiteToNew($newId, $site['idSite']);
        }
        
        self::$is_request_ok = true;
        return 1;
    }
    
    public static function saveNewSiteInfo()
    {
        $newId = self::$data['new_id'];
        $siteId = self::$data['site_id'];
        $link = self::$data['link'];
        $status = self::$data['status'];
        $comment = self::$data['comment'];
        
        
        // TODO: Validation
        $link = urlencode($link);
        
        $result = SitesModel::updateNewSiteInfo($newId, $siteId, $link, $status, $comment);
        if (!$result)
            return "Error";
       
        self::$is_request_ok = true;
        return 1;
    }
    
    public static function makeReport()
    {
        require_once "libs/modules/dompdf/dompdf_config.inc.php";
        
        $newId = self::$data['new_id'];
        $newSites = SitesModel::getNewSites($newId);
        
        $html = SitesComponent::formReportContent($newSites);

        $dompdf = new DOMPDF();
        
        $dompdf->load_html($html);
        $dompdf->render();

        $dompdf->stream("report.pdf");
    }
}

?>
