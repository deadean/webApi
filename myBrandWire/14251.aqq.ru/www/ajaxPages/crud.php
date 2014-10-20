<?php session_save_path("../tmp");session_start();?>
<?php

	include_once '../lib/csFunctions.php';
	include_once '../model/mainController.php';
	include_once '../model/controller.php';
    include_once '../model/CActionManager.php';
	include_once '../lib/php/Interfaces/Recovery/CRecoveryBase.php';

	$controller = new Controller();
	$con = $controller->GetConnection();

	//print_r($_GET);
	//print_r($_POST);
	//print_r(json_decode($_GET["param"]));
	
	$id = mysqli_real_escape_string($con,$_GET["param"]);
	$action = mysqli_real_escape_string($con,$_GET["action"]);
	if($action==NULL){
		$action = mysqli_real_escape_string($con,$_POST["action"]);
	}
	
	if(is_null($id) || is_null($action))
		return;

    $actionManager = new CActionManager(new Model());
    $actionManager->DoActionByObject($action,json_decode(json_encode($_POST),false));
	
	if($action=="removeNews"){
		$controller->RemoveNews($id);
		return;
	}
	if($action=="publishNews"){
		$controller->PublishNews($id);
		return;
	}
	if($action=="updateCompanySocialContacts"){
		$controller->UpdateCompanySocialContacts(mysqli_real_escape_string($con,$_POST["idCompany"]),$_POST["name"],$_POST["type"]);
		return;
	}
	if($action=="getCompanyInfo"){
		$idCompany = ClearParam($_POST["idCompany"],$con);
		$company = $controller->GetCompanyByCashe($idCompany);
		echo json_encode($company);
		return;
	}
	if($action=="createContactByCompany"){
        $idContactIn = ClearParam($_POST["idContactId"],$con) == "default"
            ? ClearParam($_POST["idContactId"],$con) : ClearParam($_POST["idContact"],$con);
		$idContact = $controller->CreatePersonContactByCompany
		(
			ClearParam($_POST["idCompany"],$con),$idContactIn,ClearParam($_POST["name"],$con),ClearParam($_POST["position"],$con),
			ClearParam($_POST["email"],$con),ClearParam($_POST["phone"],$con),ClearParam($_POST["skype"],$con)
		);
        if($idContactIn=="default"){
            $uploaddir = '../uploads/companyContacts/';
            $uploadfile = $uploaddir . "" . $idContact . "" . ".jpg";
            move_uploaded_file($_FILES['ImageFile']['tmp_name'], $uploadfile);
        }

		$idCompany = ClearParam($_POST["idCompany"],$con);
		$company = $controller->GetCompanyByCashe($idCompany);
		echo json_encode($company->logo);
		return;
	}
    if($action=="updateCompanyImg"){
		$obj = new DateTime('now');
        $uploaddir = '../uploads/';
        $uploadfile = $uploaddir . "id_" . $_POST["idCompany"] . "_" . $obj->format('d_m_Y_H_m_s'). ".jpg";
        $previousfile = $_POST["previouslogo"];
        move_uploaded_file($_FILES['ImageFile']['tmp_name'], $uploadfile);
		$controller->UpdateCompanyLogo($_POST["idCompany"],$uploadfile);
		unlink($previousfile);
		echo json_encode($uploadfile);
    }
	if($action=="createAddressByCompany"){
		$controller->CreateAddressByCompany
		(
			ClearParam($_POST["idCompany"],$con),ClearParam($_POST["companyRegion"],$con),ClearParam($_POST["companyCommunity"],$con),
			ClearParam($_POST["companyEmail"],$con),ClearParam($_POST["companyPhone"],$con),ClearParam($_POST["companySkype"],$con),
            ClearParam($_POST["companyName"],$con)
		);
		$idCompany = ClearParam($_POST["idCompany"],$con);
		$company = $controller->GetCompanyByCashe($idCompany);
		echo json_encode($company->logo);
		return;
	}
	if($action=="addNewCompanyToUser"){
		$company = new Company();
		$company->name = ClearParam($_POST["txtNameCompany"],$con);
		$company->about =ClearParam($_POST["txtAboutCompany"],$con);
		$controller->CreateCompanyByUser(ClearParam($_POST["idUser"],$con),$company);
		echo 
				"
					<form action='../company/company.php' method='POST' id='reqForm'>
						<input type='hidden' name='login' value=".$_POST["login"]." />
						<input type='hidden' name='password' value=".$_POST["password"]." />
					</form>
					<script>document.getElementById('reqForm').submit();</script>
				";
		return;
	}
	if($action=="isUserRegister"){
		$res = $controller->IsLoginRegister($id);
		if($res=='1')
			echo "error";
		return;
	}
	if($action=="CheckIsEmailRegister"){
		$userEmail = $_POST["userEmail"];
		$res = $controller->IsEmailRegister($userEmail);
		if($res['isExist']=="1"){
			$recoveryInfo = new \php\Interfaces\Recovery\CRecoveryBase();
			$recoveryInfo->userEmail = $userEmail;
			$recoveryInfo->idUser = $res['idUser'];
			$recoveryInfo->sendTime = time();

			$emailObject = new Email();
			$emailObject->To =$userEmail;
			$emailObject->From = csConstants::$csCompanyEmail;
			$emailObject->Subject = "Восставноление доступа";
			$emailObject->Message =
				"
					На сайте MyBrandWire была запущена процедура восстановления пароля.</br>
					Для задания нового пароля для пользователя ".$userEmail.", перейдите по ссылке:
					http://".$_SERVER["SERVER_NAME"]."/recpass/recpass.php?id=".$recoveryInfo->GetHashCode()."
					Ссылка для задания нового пароля будет активна 24 часа.
					Если вы не запускали процесс восстановления пароля, проигнорируйте данное письмо.
				";
			$emailObject->Send();
			$recoveryInfo->Save();
		}
		echo json_encode($res['isExist']);
	}
	if($action=="checkIsEmailRegister"){
		$userEmail = $_POST["newUserEmail"];
		$res = $controller->IsEmailRegister($userEmail);
		echo json_encode($res['isExist']);
	}
	if($action=="recoverPassword"){
		$recoveryInfo = new \php\Interfaces\Recovery\CRecoveryBase();
		$recoveryInfo->idUser = $_POST['idUser'];
		$recoveryInfo->Remove();

		$user = $controller->UpdateUsersPassword($_POST['idUser'],md5($_POST['pass1']));
		$_SESSION["idUser"] = $user -> id;
		$_SESSION["loginUser"] = $user -> login;
		$_SESSION["userObject"] = serialize($user);
		if($user==NULL){
			echo "0";
			return;
		}

		echo "1";
		return;
	}
	if($action=="sortMainNewsByCategoryId"){
		$news = $controller -> GetPaymentNewsByCategoryId($id);
		$news = array_slice($news, 0,3);
		include "../mainNews/mainNewsForm.php";
		return;
	}
    if($action=="sortMainNewsByDate"){
		$news = $controller -> GetPaymentNews();
		$news = array_slice($news, 0,3);
		include "../mainNews/mainNewsForm.php";
		return;
	}
    if($action=="PreviewOrder"){
        $isHideHeaderAndFooter = "1";
        include "../news/previewNewsForm.php";
        return;
    }
	if($action=="sortFreeNewsByCategoryId"){
		$news = $controller -> GetFreeNewsByCategoryId($id);
		$news = array_slice($news, 0,3);
		include "../freeNews/freeNewsForm.php";
		return;
	}
	if($action=="sortFreeNewsByDate"){
		$news = $controller -> GetFreeNews();
		$news = array_slice($news, 0,3);
		include "../freeNews/freeNewsForm.php";
		return;
	}
	if($action=="showMainNewsPortionSize"){
		$idCategory = ClearParam($_POST["idCategory"],$con);
		$portionSize = ClearParam($_POST["portionSize"],$con);
		
		if($idCategory=="0"){
			$news = $controller -> GetPaymentNews();
		}
		else{
			$news = $controller -> GetPaymentNewsByCategoryId($idCategory);
		}
		$news = array_slice($news,0,$portionSize);
		include "../mainNews/mainNewsForm.php";
		return;
	}
	if($action=="showFreeNewsPortionSize"){
		$idCategory = ClearParam($_POST["idCategory"],$con);
		$portionSize = ClearParam($_POST["portionSize"],$con);
		if($idCategory=="0"){
			$news = $controller -> GetFreeNews();
		}
		else{
			$news = $controller -> GetFreeNewsByCategoryId($idCategory);
		}
		$news = array_slice($news,0,$portionSize);
		include "../freeNews/freeNewsForm.php";
		return;
	}
	if($action=="find"){
		$id = ClearParam($_POST["value"],$con);
		$place = ClearParam($_POST["place"],$con);
		$id=trim($id);
		
		//echo $place;
		
		if($id=="")
		{
			$path='';
			$path = $place=="/index.php"?'../ajaxPages/indexMainContent.php':$path;
			$path = $place=="/about/about.php"?'../about/aboutMainContent.php':$path;
			$path = $place=="/services/services.php"?'../services/servicesMainContent.php':$path;
			$path = $place=="/blog/blog.php"?'../blog/blogMainContent.php':$path;
			$path = $place=="/mainNews/mainNews.php"?'../mainNews/mainNewsMainContent.php':$path;
			$path = $place=="/news/mainNewsForm.php"?'../news/mainNewsFormMainContent.php':$path;
			$path = $place=="/freeNews/freeNews.php"?'../freeNews/freeNewsMainContent.php':$path;
            $path = $place=="/about/howthiswork.php"?'../about/howthisworkContent.php':$path;
			
			include_once $path;
			return;
		}
		
		$news = $controller -> FindNews($id); 
		
		include_once '../ajaxPages/foundedNews.php';
		
		return;
	}
	if($action=="addSocialNetworksToCompany"){
		$ids = array_slice(explode("_", $_POST["idsParam"]),0,-1);
		if(count($ids)!=0){
			$controller->AddSocialNetworksToCompany(ClearParam($_POST["idCompany"],$con),$ids);
		}
		$idCompany = ClearParam($_POST["idCompany"],$con);
		$company = $controller->GetCompanyByCashe($idCompany);
		echo json_encode($company);
		return;
	}
	if($action=="deleteContactByCompany"){
		$controller->DeleteContactByCompany(ClearParam($_POST["idCompany"],$con),ClearParam($_POST["idContact"],$con));
		$idCompany = ClearParam($_POST["idCompany"],$con);
		$company = $controller->GetCompanyByCashe($idCompany);
		echo json_encode($company->logo);
		return;
	}
	if($action=="showCompanyContacts"){
        $companyContacts = $controller->getInstance()->GetCompanyById($id)->persons;
        if($companyContacts!=NULL){
            include_once 'showCompanyContacts.php';
        }
        return;
    }
    if($action=="checkCompanyInfo"){
        $company = $controller->getInstance()->GetCompanyById($_POST["selectedCompanyId"]);
        $type = $_POST["type"];

        echo $type;

        $text1 = $type!="checkboxSocialNetworks6" ? "NONE" : "0";
        if($text1 == "0")
            $text1 = $company->about==NULL ? "1" : "0";
        echo "<div id='idCompanyAbout'>".$text1."</div>";

        $text1 = $type!="checkboxSocialNetworks7" ? "NONE" : "0";
        if($text1 == "0")
            $text1 = $company->region==NULL ? "1" : "0";
        echo "<div id='idCompanyAddress'>".$text1."</div>";

        $text1 = $type!="checkboxAddCompanySocialNetworks" ? "NONE" : "0";
        if($text1 == "0")
            $text1 = $company->IsSocialNetworksExist()==0 ? "1" : "0";
        echo "<div id='idCompanySocNetworks'>".$text1."</div>";
        return ;
    }

?>