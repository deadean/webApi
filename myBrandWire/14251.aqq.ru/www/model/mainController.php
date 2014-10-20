<?php
	session_start();
    include_once "controller.php";
	include_once "../lib/csFunctions.php";
	include_once 'model.php';
    include_once '../lib/php/Interfaces/Cookies/ICookieService.php';
    include_once '../lib/php/Interfaces/Cookies/CookieBase.php';
    include_once '../lib/php/Implementations/Cookies/CCookiesService.php';
    include_once '../lib/php/Implementations/Cookies/CCookie.php';

	$controller = Controller::getInstance();
	$action = $_POST["action"];
	$actionGet = $_GET["action"];
	$con = $controller->GetConnection();
    $cookieService = new \Cookies\CCookiesService();
	
	//print_r($_GET);
	//print_r($_POST);
    //print_r($_COOKIE);
	//Alert($action);

	if($action=="save"){
		
		$nameUser = mysqli_real_escape_string($con, $_POST["txtNameUser"]);
		$surnameUser = mysqli_real_escape_string($con,$_POST["txtSurnameUser"]);
		$emailUser = mysqli_real_escape_string($con,$_POST["txtEmailUser"]);
		$phoneUser = mysqli_real_escape_string($con,$_POST["txtPhoneUser"]);
		
		$name=mysqli_real_escape_string($con,$_POST["txtNameCompany"]);
		$logo = $_FILES['userfile']['tmp_name'];
		
		//Alert($logo);
		$about = mysqli_real_escape_string($con,$_POST["txtAboutCompany"]);
		$login = mysqli_real_escape_string($con,$_POST["txtLoginCompany"]);
		$password = mysqli_real_escape_string($con,$_POST["txtPasswordCompany"]);
		
		$region = mysqli_real_escape_string($con,$_POST["regionSelect"]);
		$community = mysqli_real_escape_string($con,$_POST["communitySelect"]);
		
		$UserId = $controller->AddNewCompanyAndLogin($nameUser,$surnameUser,$emailUser,$phoneUser,$login,$password,$name,$logo,$about,$region,$community);
		
		$uploaddir = '../uploads/';
		$uploadfile = $uploaddir . "id_" . $UserId . "_" . ".jpg";// basename($_FILES['userfile']['name']);
		move_uploaded_file($_FILES['userfile']['tmp_name'], $uploadfile); 
		
		echo "<script> window.location='../index.php?action=messageRegisterShow'; </script>";
		exit;
	}

	if($action=="login"){
        $isRememberMe = isset($_POST["IsRememberMe"]);
		$login = mysqli_real_escape_string($con, $_POST["login"]);
		$password = mysqli_real_escape_string($con, $_POST["password"]);

        if($login=="admin" && $password == "admin"){
            echo "<script> window.location='../client/index.html'; </script>";
            exit;
        }
		$isUserRegistered = $controller->IsUserRegistered($login,md5($password));
        if($isRememberMe){
            $cookieInfo = new \php\Implementations\Cookies\CCookie();
            $cookieInfo->login = $login;
            $cookieInfo->password = $password;
            $cookieService->SetCookiesForUser($cookieInfo);
        }

		//print_r($_SESSION);

		if($isUserRegistered){
			echo "<script> window.location='../company/company.php'; </script>";
			exit;
		}
		else{
			echo "<script> window.location='../login/login.php?error=1'; </script>";
			exit;
		}
	}

	if($actionGet=="messageRegisterShow"){
		Alert($actionGet);
		CheckForMessagies();	
	}

	if($action=="showNewsForm"){
		echo "<script> window.location='news/newsAdaptee.php'; </script>";
		exit;
	}
	
	if($action=="savePreNews"){
		//print_r($_POST);
		$controller->CreatePreNews($_POST["spellingTasks"],$_POST["dateStart"],$_POST["dateEnd"],$_POST["region"],$_POST["event"],
		$_POST["fact"],$_POST["commonComments"],$_POST["text"],$_POST["contactStatus"],$_POST["contactPhone"],$_POST["contactEmail"],$_POST["links"]);
		echo "<script> window.location='../index.php'; </script>";
		exit;
	}

	if($action=="publish"){
		$order = unserialize($_SESSION["order"]);
		$order->datePublish = $_POST["datePublish"];
        $order->totalCost = $_POST["TotalDollarCost"];
        $order->news->content = $order->payment==0?preg_replace("/<img[^>]+\>/i", "", $order->news->content):$order->news->content;
        $controller->AddOrder($order);
		echo "<script> window.location='../company/company.php'; </script>";
		exit;
	}
	
	if($action=="edit"){
		$editableNews = unserialize($_SESSION["editableNews"]);

		$editableNews->Save();
		echo "<script> window.location='../company/company.php'; </script>";
		exit;
	}
	
	if($action=="sendEmailToUs"){
		
//		$name = $_POST["name"];
//		$email = mysqli_real_escape_string($con, $_POST["email"]);
//		$text = $_POST["text"];
//		$emailObject = new Email();
//		$email->To = csConstants::$csCompanyEmail;
//		//$emailObject->To = "deadean@yandex.ru";
//		$emailObject->From = $email;
//		$emailObject->Subject = $name;
//		$emailObject->Message = $text;
//		$emailObject->Send();

		$emailObject = new Email();
		$emailObject->To =csConstants::$csCompanyEmail;
		$emailObject->From = mysqli_real_escape_string($con, $_POST["email"]);
		$emailObject->Subject = "Пользователь".$_POST["name"];
		$emailObject->Message ="".$_POST["text"];
		$emailObject->Send();
		
		echo "<script> window.location='../index.php'; </script>";
		exit;
	}
?>
