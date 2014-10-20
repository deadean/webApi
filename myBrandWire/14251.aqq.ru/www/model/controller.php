<?php session_save_path("../tmp");session_start(); ?>
<?php
include "conn.inc";
include "model.php";
class Controller {
	protected static $_instance;
	private $_countries;

	public function __construct() {//Alert("Constructor Controller");PrintStackInclue();
	}

	private function __clone() {
	}

	public static function getInstance() {

		if (null === self::$_instance) {
			//Alert("new Controller");
			self::$_instance = new self();
			//PrintStackInclue();
		}
		return self::$_instance;
	}

	function AddNewCompany($name, $surname, $email, $phone) {
        //$model = new Model();
		//$model -> AddNewCompany($name, $surname, $email, $phone);
	}

	function AddNewCompanyAndLogin($nameUser, $surnameUser, $emailUser, $phoneUser, $login, $password, $name, $logo, $about, $region, $community) {
		$model = new Model();

		$userId = $model -> AddNewUser($nameUser, $surnameUser, $emailUser, $phoneUser, $login, $password);
		$res = $model -> AddNewCompany($name, $logo, $about, $region, $community, $userId);
		$model -> SendEmailWithRegistrationLink($emailUser);
		return $res;
	}

	function AddOrder($order) {
		//print_r($order);
		$model = new Model();
		$model -> AddOrder($order);
	}
	
	function AddSocialNetworksToCompany($idCompany,$ids) {
		$model = new Model();
		$model -> AddSocialNetworksToCompany($idCompany,$ids);
	}

	function AddUserToSession($user){
		$_SESSION["idUser"] = $user -> id;
		$_SESSION["loginUser"] = $user -> login;
		$_SESSION["userObject"] = serialize($user);
	}
	
	function IsUserRegistered($login, $password) {
		$model = new Model();
		$user = $model -> IsUserRegistered($login, $password);

		//print_r($user);
		if ($user=="") {
			return FALSE;
		}

		$this->AddUserToSession($user);

		return TRUE;
	}

	function UpdateUsersPassword($idUser, $password){
		$model = new Model();
		$user = $model->UpdateUserPassword($idUser,$password);

		if ($user=="") {
			return NULL;
		}

		//$this->AddUserToSession($user);
		return $user;

	}

	function UpdateCompanyLogo($idCompany, $logo){
		Model::getInstance()->UpdateCompanyLogo($idCompany,$logo);
	}
	
	function IsLoginRegister($login) {
		$model = new Model();
		return $model -> IsLoginRegister($login);
	}

	function IsEmailRegister($email) {
		$model = new Model();
		return $model -> IsEmailRegister($email);
	}

	function CreatePreNews($tasks, $dateStart, $dateEnd, $region, $event, $fact, $commonComments, $text, $contactStatus, $contactPhone, $contactEmail, $links) {
		$file = "file.html";
		$res = "
			<html>
 				<head>
  					<title>myBrandWire</title>
  					<meta http-equiv='content-type' content='text/html; charset=UTF-8' />
  				
			<style>
				.container{
					position: relative;
				}
				.container > div{
					display: table-cell;
					position: static;
					vertical-align: middle;
				}
				.commonTextFont{
					text-decoration: none;
					font-family: Arial, Helvetica, sans-serif;
				}
				.boldText{
					font-weight: bold;
				}
				.textLeft{
					text-align: left;
				}
				.leftSpace{
					padding-left: 5;
					padding-right: 5;
				}
				.blockLeft{
					position: relative;
					float: left;
				}
				.blockPadding{
					padding: 5;
				}
				.commonInputTextBox{
					height: 34;
					border: 1px solid #ccc;
					padding: 5;
				}
				
			</style>
			</head>
			<body>
			<div class='container' >
  			
  			<div class='' style='width: 200;'>
  				<div class='commonTextFont boldText textLeft leftSpace' style='height: 55;'>Задачи и условия :</div>
  				<div style='height: 10;'></div>
  				<div class='commonTextFont boldText textLeft leftSpace ' style='height: 40'>Дата события :</div>
  				<div style='height: 10;'></div>
  				<div class='commonTextFont boldText textLeft leftSpace ' style='height: 40'>Регион :</div>
  				<div style='height: 10;'></div>
  				<div class='commonTextFont boldText textLeft leftSpace ' style='height: 55'>Событие / информационный повод :</div>
  				<div style='height: 10;'></div>
  				<div class='commonTextFont boldText textLeft leftSpace' style='height: 55;'>Значимость факта для аудитории :</div>
  				<div style='height: 10;'></div>
  				<div class='commonTextFont boldText textLeft leftSpace ' style='height: 55;'>Подробности обязательные для упоминания :</div>
  				<div style='height: 10;'></div>
  				<div class='commonTextFont boldText textLeft leftSpace ' style='height: 55;'>Цитата :</div>
  				<div style='height: 10;'></div>
  				<div class='commonTextFont boldText textLeft leftSpace ' style='height: 120;'>Контактное лицо для связи при возникновении вопросов :</div>
  				<div style='height: 10;'></div>
  				<div class='commonTextFont boldText textLeft leftSpace ' style='height: 40;'>Ссылки на изображения / видео :</div>
  			</div>
  			
  			<div class=''>
  				<div class='commonTextFont' style='height: 55; width:760'>
  					<textarea class='blockLeft' rows='3' cols='75' >$tasks</textarea>
  				</div>
  				<div style='height: 10;'></div>
  				<div class='commonTextFont textLeft container ' style='height: 40'>
  					<div><input type='date' value='$dateStart' /></div>
  					<div class='blockPadding'>по</div>
  					<div><input type='date' value='$dateEnd' /></div>
  				</div>
  				<div style='height: 10;'></div>
  				<div class='commonTextFont textLeft ' style='height: 40;padding-left: 1'>
  					<input type='text' class='commonInputTextBox' style='width: 320' value='$region' />
  				</div>
  				<div style='height: 10;'></div>
  				<div class='commonTextFont' style='height: 55; width:760'>
  					<textarea class='blockLeft' rows='3' cols='75' name='spellingTasks' value=''>$event</textarea>
  				</div>
  				<div style='height: 10;'></div>
  				<div class='commonTextFont' style='height: 55; width:760'>
  					<textarea class='blockLeft' rows='3' cols='75' name='spellingTasks' value=''>$fact</textarea>
  				</div>
  				<div style='height: 10;'></div>
  				<div class='commonTextFont' style='height: 55; width:760'>
  					<textarea class='blockLeft' rows='3' cols='75' name='spellingTasks' value=''>$commonComments</textarea>
  				</div>
  				<div style='height: 10;'></div>
  				<div class='commonTextFont' style='height: 55; width:760'>
  					<textarea class='blockLeft' rows='3' cols='75' name='spellingTasks' value=''>$text</textarea>
  				</div>
  				<div style='height: 10;'></div>
  				<div class='commonTextFont boldText textLeft leftSpace ' style='height: 120;padding: 2'>
  					<div><input type='text' class='commonInputTextBox ' style='width: 320;margin-bottom: 10' value='$contactStatus'/></div>
  					<div><input type='text' class='commonInputTextBox ' style='width: 320;margin-bottom: 10' value='$contactPhone'/></div>
  					<div><input type='text' class='commonInputTextBox ' style='width: 320' value='$contactEmail'/></div>
  				</div>
  				<div style='height: 10;'></div>
  				<div class='commonTextFont boldText textLeft leftSpace ' style='height: 40;padding: 2'>
  					<input type='text' class='commonInputTextBox ' style='width: 320;margin-bottom: 10' value='$links'/>
  				</div>
  			</div>
  		</div></body></html>
			";
		file_put_contents($file, $res);
	}

	function CreateNewOrder($newsHeader, $newsCommon, $newsContent, $newsTags, $categoryId,
                            $companyId, $comments,$isPayment,$isAutoAddCompanySocialNetworks,$contactsIds,
                            $isAutoAddAboutCompanyInfo,$isAutoAddCompanyAddress) {

		$con = $this -> GetConnection();
		$categoryId = explode(";", $categoryId);
		$newsHeader = mysqli_real_escape_string($con, $newsHeader);
		$newsCommon = mysqli_real_escape_string($con, $newsCommon);
		$newsContent = mysqli_real_escape_string($con, $newsContent);
		$newsTags = mysqli_real_escape_string($con, $newsTags);
		$comments = mysqli_real_escape_string($con, $comments);

		$order = new Order();
		$order -> payment = $isPayment;
        $order -> isAutoAddCompanySocialNetworks = $order->ClearParam($isAutoAddCompanySocialNetworks,$con);
        $order -> isAutoAddAboutCompanyInfo = $order->ClearParam($isAutoAddAboutCompanyInfo,$con);
        $order -> isAutoAddCompanyAddress = $order->ClearParam($isAutoAddCompanyAddress,$con);
        $order->contactsIds = $contactsIds;
		
		$news = new News();
		$news -> header = $newsHeader;
		$news -> content = $newsContent;
		$news -> common = $newsCommon;
		$news -> tags = $newsTags;
		$news -> comments = $comments;
        $news -> status = $order -> payment == 1 ? "waiting_for_payment" : "spell_checked";

		$cats = $this -> GetCategories();
		foreach ($categoryId as $key1 => $value1) {
			foreach ($cats as $key => $value) {
				if ($value -> id == $value1) {
					$news -> categories[$value1] = $value;
					break;
				}
			}
		}

		$order -> news = $news;
		return $order;
	}

	public function CreatePersonContactByCompany($idCompany,$idContact, $name, $position, $email, $phone, $skype) {
		$model = new Model();
		return $model -> AddPersonContactByCompany($idCompany,$idContact, $name, $position, $email, $phone, $skype);
	}
	public function CreateAddressByCompany($idCompany,$companyRegion,$companyCommunity,$companyEmail,$companyPhone,$companySkype,$companyName) {
		$model = new Model();
		$model -> CreateAddressByCompany($idCompany,$companyRegion,$companyCommunity,$companyEmail,$companyPhone,$companySkype,$companyName);
	}
	
	public function CreateCompanyByUser($idUser, $company) {
		$model = new Model();
		$model -> AddNewCompany($company->name,"",$company->about,"","",$idUser);
	}

    function GetCompanyById($id){
        $model = new Model();
        return $model -> GetCompanyById($id);
    }

	function GetCompanyByCashe($id){
		$model = new Model();
		return $model -> GetCompanyByCashe($id);
	}

	function GetNewsById($id) {
		$model = new Model();
		return $model -> GetNewsById($id);
	}

	public function GetNewsByCompany($idCompany) {
		$model = new Model();
		return $model -> GetNewsByCompany($idCompany);
	}

	function GetPaymentNews() {
		$model = new Model();
		return $model -> GetPaymentNews();
	}
	
	function GetPaymentNewsByCategoryId($idCategory) {
		$model = new Model();
		return $model -> GetPaymentNewsByCategoryId($idCategory);
	}

	function GetFreeNews() {
		$model = new Model();
		return $model -> GetFreeNews();
	}
	
	function GetFreeNewsByCategoryId($idCategory) {
		$model = new Model();
		return $model -> GetFreeNewsByCategoryId($idCategory);
	}

	function GetBlogNews() {
		$model = new Model();
		return $model -> GetBlogNews();
	}

	function GetConnection() {
		$model = new Model();
		return $model -> GetConnection();
	}

	function GetCategories() {
		$model = new Model();
		return $model -> GetCategories();
	}

	public function GetCommunitiesByCountry($idCountry) {
		$model = new Model();
		return $model -> GetCommunitiesByCountry($idCountry);
	}
	
	public function GetCommunications() {
		$model = new Model();
		return $model -> GetCommunications();
	}

	public function GetCountries() {
		if ($this -> _countries == NULL) {
			$model = new Model();
			$this -> _countries = $model -> GetCountries();
		}
		return $this -> _countries;
	}

	public function RemoveNews($id) {
		$model = new Model();
		return $model -> RemoveEntity($id, "news");
	}
	
	public function DeleteContactByCompany($idCompany,$idContact){
		$model = new Model();
		return $model -> RemoveEntity($idContact, "contact");
	}

	public function PublishNews($id) {
		$model = new Model();
		return $model -> PublishNews($id);
	}

	public function UpdateCompanySocialContacts($idCompany, $name, $type) {
		$model = new Model();
		$model -> UpdateCompanySocialContacts($idCompany, $name, $type);
	}

	public function FindNews($param) {
		$model = new Model();
		return $model -> FindNews($param);
	}

};
?>
