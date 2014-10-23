<?php

class BidsController extends Controller
{
    public static function init() 
    {
        self::$components = array();
        self::$models = array('Bids', 'Moderators');
        self::$methods_params = array(
            'getBids' => array(),
            'bidInfo' => array('id'),
            'saveBidInfo' => array('id', 'header', 'common', 'content', 'tags', 'publish_time'),
            'bindBidToModerator' => array('id'),
            'spellChecked' => array('id'),
            'approvalPassed' => array('id'),
            'getBidInfoWithOutTags' => array('id')
        );
    }
    
    public static function getBids()
    {
        // Is moderator exists
        $moderatorInfo = ModeratorsModel::getModeratorById(Config::$moderatorId);
        if (!$moderatorInfo)
            return "Moderator not found";
        
        $bids = BidsModel::getNotPublishedBidsList();
        self::$is_request_ok = true;
        return $bids;
    }
    
    public static function bidInfo()
    {
        // Is moderator exists
        $moderatorInfo = ModeratorsModel::getModeratorById(Config::$moderatorId);
        if (!$moderatorInfo)
            return "Moderator not found";
        
        // Is bid exists
        $bidId = self::$data['id'];
        $bidInfo = BidsModel::getBidInfo($bidId);
        if (!$bidInfo)
            return "Bid not found";
        
        self::$is_request_ok = true;
        return $bidInfo;
    }
    
    public static function getBidInfoWithOutTags()
    {
        // Is moderator exists
        $moderatorInfo = ModeratorsModel::getModeratorById(Config::$moderatorId);
        if (!$moderatorInfo)
            return "Moderator not found";
        
        // Is bid exists
        $bidId = self::$data['id'];
        $bidInfo = BidsModel::getBidInfo($bidId);
        if (!$bidInfo)
            return "Bid not found";

        $availableTags = "<p><a><img><ul><ol>";
        $bidContent = array(
            'header' => strip_tags(html_entity_decode($bidInfo['header']), $availableTags),
            'common' => strip_tags(html_entity_decode($bidInfo['common']), $availableTags),
            'content' => strip_tags(html_entity_decode($bidInfo['content']), $availableTags),
            'tags' => strip_tags(html_entity_decode($bidInfo['tags']), $availableTags),
        );
        
        self::$is_request_ok = true;
        return $bidContent;
    }
    
    public static function saveBidInfo()
    {
        // Is moderator exists
        $moderatorInfo = ModeratorsModel::getModeratorById(Config::$moderatorId);
        if (!$moderatorInfo)
            return "Moderator not found";
        
        $bidId = self::$data['id'];
        $header = self::$data['header'];
        $common = self::$data['common'];
        $content = self::$data['content'];
        $tags = self::$data['tags'];
        $publishTime = self::$data['publish_time'];
                
        // Is bid exists
        $bidInfo = BidsModel::getBidInfo($bidId);
        if (!$bidInfo)
            return "Bid not found";
        
        BidsModel::updateBidInfo($bidId, $header, $common, $content, $tags, $publishTime);
        self::$is_request_ok = true;
        return 1;
    }
    
    public static function bindBidToModerator()
    {
        // Is moderator exists
        $moderatorInfo = ModeratorsModel::getModeratorById(Config::$moderatorId);
        if (!$moderatorInfo)
            return "Moderator not found";
        
        $bidId = self::$data['id'];
        
        // Is bid exists
        $bidInfo = BidsModel::getBidInfo($bidId);
        if (!$bidInfo)
            return "Bid not found";
        
        $result = BidsModel::bindModeratorToBid($bidId, Config::$moderatorId);
        if (!$result)
            return "Unknown error";
        
        self::$is_request_ok = true;
        return 1;
    }
    
    public static function spellChecked()
    {
        // Is moderator exists
        $moderatorInfo = ModeratorsModel::getModeratorById(Config::$moderatorId);
        if (!$moderatorInfo)
            return "Moderator not found";
        
        $bidId = self::$data['id'];
        
        $bidInfo = BidsModel::getBidInfo($bidId);
        if (!$bidInfo)
            return 'Bid not found';
        
        if ($bidInfo['status'] != 'payed')
            return 'Wrong status';
        
        if (!$bidInfo['grammerCheck']) 
            return 'No need to check grammer';
        
        if (BidsModel::setOrthographyIsChecked($bidId)) 
        {
            self::$is_request_ok = true;
            return 1;
        }
    }
    
    public static function approvalPassed()
    {
        // Is moderator exists
        $moderatorInfo = ModeratorsModel::getModeratorById(Config::$moderatorId);
        if (!$moderatorInfo)
            return "Moderator not found";
        
        $bidId = self::$data['id'];
        $bidInfo = BidsModel::getBidInfo($bidId);
        if (!$bidInfo)
            return 'Bid not found';
        
        if ($bidInfo['status'] != 'spell_checked')
            return 'Wrong status';
        
        if (BidsModel::setApprovalPassed($bidId)) 
        {
            self::$is_request_ok = true;
            return 1;
        }
    }
}
?>
