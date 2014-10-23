<?php

class ClientsController extends Controller
{
    public static function init() 
    {
        self::$components = array();
        self::$models = array('Clients', 'Moderators', 'Company', 'Bids');
        self::$methods_params = array(
            'getClients' => array(),
            'getClientInfo' => array('client_id'),
            'sendMessageToClient' => array('client_id', 'subject', 'message')
        );
    }
    
    public static function getClients()
    {
        // Is moderator exists
        $moderatorInfo = ModeratorsModel::getModeratorById(Config::$moderatorId);
        if (!$moderatorInfo)
            return "Moderator not found";
        
        $clients = ClientsModel::getClients();
        
        self::$is_request_ok = true;
        return $clients;
    }
    
    public static function getClient()
    {
        // Is moderator exists
        $moderatorInfo = ModeratorsModel::getModeratorById(Config::$moderatorId);
        if (!$moderatorInfo)
            return "Moderator not found";
        
        $clientId = self::$data['id'];
        
        $client = ClientsModel::getClient($clientId);
        if (!$client)
            return "Client not found";
        
        $companies = CompanyModel::getClientCompanies($clientId);
        $client['companies'] = $companies;
        
        $news = BidsModel::getClientLastNews($clientId, 10);
        $client['news'] = $news;
        
        self::$is_request_ok = true;
        return $client;
    }
    
    public static function sendMessageToClient()
    {
        // Is moderator exists
        $moderatorInfo = ModeratorsModel::getModeratorById(Config::$moderatorId);
        if (!$moderatorInfo)
            return "Moderator not found";
        
        $clientId = self::$data['client_id'];
        $subject = self::$data['subject'];
        $message = self::$data['message'];
    }
};
?>
