<?php session_start(); ?>
<?php
	include_once 'CValidate.php';
	function Alert($value){
		echo "<script>alert('$value');</script>";
	}
	
	function InitGlobServerRedirectConstants(){
		$_SESSION["registerLogic"] = "../index.php";
		$_SESSION["ToRegisterFromService"] = "../register/register.php";
		
		$_SESSION["ToIndexFromChild"] = "../index.php";
		
		$_SESSION["ToServicesPage"]="../services/services.php";
		$_SESSION["ToLoginPage"]="../login/login.php";
		
		$_SESSION["ToAbout"]="about/about.php";
		$_SESSION["ToAboutFromChild"] = "../about/about.php";
		
		$_SESSION["ToCompanyFromChild"] = "../company/company.php";
		$_SESSION["ToNewsAdapteeForm"] = "news/newsAdaptee.php";
		$_SESSION["ToMainNewsForm"] = "news/mainNewsForm.php";
		$_SESSION["ToMainNews"] = "mainNews/mainNews.php";
		$_SESSION["ToBlogNewsForm"] = "news/blogNewsForm.php";
		$_SESSION["ToMainNewsFormFromChild"] = "../news/mainNewsForm.php";
		$_SESSION["ToPreviewNewsForm"] = "../../news/previewNewsForm.php";
        $_SESSION["ToHowThisWorks"] = "about/howthiswork.php";
		
		$_SESSION["ToBlogPage"] = "blog/blog.php";
	}
	
	function GetPageByPlace($var)
	{
		$levelBack="../";
		//Alert($var);
		
		if((string)$var === "MainNews/index.php")
			return $_SESSION["ToMainNews"];
		if(
			(string)$var === "MainNews/services/services.php" || (string)$var === "MainNews/about/about.php" || (string)$var === "MainNews/news/spellingNews.php" ||
			(string)$var === "MainNews/news/blogNewsForm.php" || (string)$var === "MainNews/blog/blog.php" || (string)$var === "MainNews/news/mainNewsForm.php" ||
            (string)$var === "MainNews/about/howthiswork.php" || (string)$var === "MainNews/company/companyPage.php"
			|| (string)$var === "MainNews/freeNews/freeNews.php"
		)
			return $levelBack.$_SESSION["ToMainNews"];
		if(
			(string)$var === "MainNews/company/news/newsAdaptee.php" || (string)$var === "MainNews/company/company.php"
		)
			return $levelBack.$levelBack.$_SESSION["ToMainNews"];
			
			
		if((string)$var === "Blog/index.php")
			return $_SESSION["ToBlogPage"];
		if(
			(string)$var === "Blog/services/services.php" || (string)$var === "Blog/company/news/newsAdaptee.php" || (string)$var === "Blog/about/about.php" ||
			(string)$var === "Blog/news/blogNewsForm.php" || (string)$var === "Blog/mainNews/mainNews.php" || (string)$var === "Blog/news/spellingNews.php"  ||
			(string)$var === "Blog/company/company.php" || (string)$var === "Blog/about/howthiswork.php"
            || (string)$var === "Blog/company/companyPage.php"
		)
			return $levelBack.$_SESSION["ToBlogPage"];
			
		if(
			(string)$var === "Blog/company/news/newsAdaptee.php"
		)
			return $levelBack.$levelBack.$_SESSION["ToBlogPage"];
			
		
		if((string)$var === "Main/services/services.php" || (string)$var === "Main/company/company.php"
        || (string)$var === "Main/company/companyPage.php" || (string)$var === "Main/about/howthiswork.php"
            )
			return $_SESSION["ToIndexFromChild"];
		if((string)$var === "Main/company/news/newsAdaptee.php")
			return $levelBack.$_SESSION["ToIndexFromChild"];
		if((string)$var === "Main/about/about.php")
			return $levelBack.$_SESSION["ToIndexFromChild"];
		if((string)$var === "Main/news/mainNewsForm.php")
			return $levelBack.$_SESSION["ToIndexFromChild"];
		if((string)$var === "Main/news/blogNewsForm.php")
			return $levelBack.$_SESSION["ToIndexFromChild"];
		if((string)$var === "Main/news/previewNewsForm.php")
			return $levelBack.$_SESSION["ToIndexFromChild"];
		if((string)$var === "Main/news/spellingNews.php")
			return $levelBack.$_SESSION["ToIndexFromChild"];
		if((string)$var === "Main/blog/blog.php")
			return $levelBack.$_SESSION["ToIndexFromChild"];
		if((string)$var === "Main/mainNews/mainNews.php")
			return $levelBack.$_SESSION["ToIndexFromChild"];
		if((string)$var === "Main/freeNews/freeNews.php")
			return $levelBack.$_SESSION["ToIndexFromChild"];
		
		
		if((string)$var === "Register/services/services.php")
			return $_SESSION["ToRegisterFromService"];
		if((string)$var === "Register/company/company.php")
			return $_SESSION["ToRegisterFromService"];
		if((string)$var === "Register/company/news/newsAdaptee.php")
			return $levelBack.$_SESSION["ToRegisterFromService"];
		if((string)$var === "Register/about/about.php")
			return $levelBack.$_SESSION["ToRegisterFromService"];
		if((string)$var === "Register/index.php")
			return $levelBack.$_SESSION["ToRegisterFromService"];
		if((string)$var === "Register/news/mainNewsForm.php")
			return $levelBack.$_SESSION["ToRegisterFromService"];
		if((string)$var === "Register/news/blogNewsForm.php")
			return $levelBack.$_SESSION["ToRegisterFromService"];
		if((string)$var === "Register/news/previewNewsForm.php")
			return $levelBack.$_SESSION["ToRegisterFromService"];
		if((string)$var === "Register/news/spellingNews.php")
			return $levelBack.$_SESSION["ToRegisterFromService"];
		if((string)$var === "Register/blog/blog.php" || (string)$var === "Register/mainNews/mainNews.php" || (string)$var === "Register/login/login.php" 
			|| (string)$var === "Register/freeNews/freeNews.php" || (string)$var === "Register/about/howthiswork.php"
            || (string)$var === "Register/company/companyPage.php"
        )
			return $levelBack.$_SESSION["ToRegisterFromService"];

				
		if((string)$var === "Login/services/services.php")
			return $_SESSION["ToLoginPage"];
		if((string)$var === "Login/login/login.php")
			return $_SESSION["registerLogic"];
		if((string)$var === "Login/company/company.php")
			return $_SESSION["ToLoginPage"];
		if((string)$var === "Login/about/about.php")
			return $levelBack.$_SESSION["ToLoginPage"];
		if((string)$var === "Login/index.php")
			return $levelBack.$_SESSION["ToLoginPage"];
		if((string)$var === "Login/news/mainNewsForm.php")
			return $levelBack.$_SESSION["ToLoginPage"];
		if((string)$var === "Login/news/blogNewsForm.php")
			return $levelBack.$_SESSION["ToLoginPage"];
		if((string)$var === "Login/news/previewNewsForm.php")
			return $levelBack.$_SESSION["ToLoginPage"];
		if((string)$var === "Login/news/spellingNews.php")
			return $levelBack.$_SESSION["ToLoginPage"];
		if((string)$var === "Login/blog/blog.php" || (string)$var === "Login/mainNews/mainNews.php" || (string)$var === "Login/freeNews/freeNews.php"
            || (string)$var === "Login/about/howthiswork.php" || (string)$var === "Login/company/companyPage.php"
        )
			return $levelBack.$_SESSION["ToLoginPage"];
		
		
		if((string)$var === "Services/company/company.php")
			return $_SESSION["ToServicesPage"];
		if((string)$var === "Services/index.php")
			return $_SESSION["ToServicesPage"];
		if((string)$var === "Services/company/news/newsAdaptee.php")
			return $levelBack.$_SESSION["ToServicesPage"];
		if((string)$var === "Services/about/about.php")
			return $levelBack.$_SESSION["ToServicesPage"];
		if((string)$var === "Services/news/mainNewsForm.php")
			return $levelBack.$_SESSION["ToServicesPage"];
		if((string)$var === "Services/news/blogNewsForm.php")
			return $levelBack.$_SESSION["ToServicesPage"];
		if((string)$var === "Services/news/previewNewsForm.php")
			return $levelBack.$_SESSION["ToServicesPage"];
		if((string)$var === "Services/news/spellingNews.php")
			return $levelBack.$_SESSION["ToServicesPage"];
		if((string)$var === "Services/blog/blog.php" || (string)$var === "Services/mainNews/mainNews.php"
            || (string)$var === "Services/about/howthiswork.php" || (string)$var === "Services/company/companyPage.php")
			return $levelBack.$_SESSION["ToServicesPage"];
		
		if((string)$var === "About/index.php")
			return $_SESSION["ToAbout"];
		if((string)$var === "About/services/services.php")
			return $_SESSION["ToAboutFromChild"];
		if((string)$var === "About/company/company.php")
			return $_SESSION["ToAboutFromChild"];
		if((string)$var === "About/login/login.php")
			return $_SESSION["ToAboutFromChild"];
		if((string)$var === "About/news/mainNewsForm.php")
			return $_SESSION["ToAboutFromChild"];
		if((string)$var === "About/news/blogNewsForm.php")
			return $_SESSION["ToAboutFromChild"];
		if((string)$var === "About/news/previewNewsForm.php")
			return $_SESSION["ToAboutFromChild"];
		if((string)$var === "About/news/spellingNews.php")
			return $_SESSION["ToAboutFromChild"];
		if((string)$var === "About/blog/blog.php" || (string)$var === "About/mainNews/mainNews.php"
            || (string)$var === "About/about/howthiswork.php" || (string)$var === "About/company/companyPage.php"
        )
			return $_SESSION["ToAboutFromChild"];
			
		if((string)$var === "Company/index.php")
			return $_SESSION["ToCompanyFromChild"];
		if((string)$var === "Company/login/login.php")
			return $_SESSION["ToCompanyFromChild"];
		if((string)$var === "Company/company/company.php")
			return $_SESSION["ToCompanyFromChild"];
		if((string)$var === "Company/about/about.php")
			return $_SESSION["ToCompanyFromChild"];
		if((string)$var === "Company/services/services.php")
			return $_SESSION["ToCompanyFromChild"];
		if((string)$var === "Company/news/previewNewsForm.php")
			return $_SESSION["ToCompanyFromChild"];
		if((string)$var === "Company/company/news/newsAdaptee.php")
			return $levelBack.$_SESSION["ToCompanyFromChild"];
		if((string)$var === "Company/blog/blog.php" || (string)$var === "Company/freeNews/freeNews.php" || (string)$var === "Company/news/mainNewsForm.php"
            || (string)$var === "Company/about/howthiswork.php" || (string)$var === "Company/company/companyPage.php"
        )
			return $levelBack.$_SESSION["ToCompanyFromChild"];
		
		if((string)$var === "ShowNewsAdapteeForm/company/company.php")
			return $_SESSION["ToNewsAdapteeForm"];
		if((string)$var === "ShowPreviewNewsForm/company/news/newsAdaptee.php")
			return $_SESSION["ToPreviewNewsForm"];
		
		if((string)$var === "MainNewsForm/index.php"){
			return $_SESSION["ToMainNewsForm"];
		}
		if((string)$var === "MainNewsForm/mainNews/mainNews.php"  || (string)$var === "MainNewsForm/ajaxPages/crud.php"
        ||(string)$var === "MainNewsForm/company/companyPage.php"
        )
        {
			return $levelBack.$_SESSION["ToMainNewsForm"];
		}
		if((string)$var === "MainNewsForm/freeNews/freeNews.php"){
			return $levelBack.$_SESSION["ToMainNewsForm"];
		}
		
		if((string)$var === "BlogNewsForm/blog/blog.php" || (string)$var === "BlogNewsForm/about/about.php"){
			return $levelBack.$_SESSION["ToBlogNewsForm"];
		}
		if((string)$var === "MainNewsForm/news/mainNewsForm.php"){
			return $_SESSION["ToMainNewsFormFromChild"];
		}

        if((string)$var === "HowThisWorks/index.php"){
            return $_SESSION["ToHowThisWorks"];
        }

        if((string)$var === "HowThisWorks/index.php" || (string)$var === "HowThisWorks/company/company.php"
            || (string)$var === "HowThisWorks/company/news/newsAdaptee.php" || (string)$var === "HowThisWorks/about/about.php"
            || (string)$var === "HowThisWorks/news/mainNewsForm.php" || (string)$var === "HowThisWorks/news/blogNewsForm.php"
            || (string)$var === "HowThisWorks/news/previewNewsForm.php" || (string)$var === "HowThisWorks/news/spellingNews.php"
            || (string)$var === "HowThisWorks/blog/blog.php" || (string)$var === "HowThisWorks/mainNews/mainNews.php"
            || (string)$var === "HowThisWorks/company/companyPage.php" || (string)$var === "HowThisWorks/services/services.php"
        )
        {
            return $levelBack.$_SESSION["ToHowThisWorks"];
        }

	}
	
	function CheckForMessagies()
	{
		echo "<script>ChangeVisibleStatus('popUpMessage');</script>";
	}
	
	function PrintArray($array){
		foreach ($array as $key=>$val)
    		echo $key." ".$val."</br>";
	}
	
	function PrintStackInclue()
	{
		$trace = debug_backtrace();
	    echo '<pre>';
	    $sb = array();
	    foreach($trace as $item) {
	        if(isset($item['file'])) {
	            $sb[] = htmlspecialchars("$item[file]:$item[line]");
	        } else {
	            $sb[] = htmlspecialchars("$item[class]:$item[function]");
	        }
	    }
	    echo implode("\n",$sb);
	    echo '</pre>';
	}
	
	function ClearParam($param,$con){
		return mysqli_real_escape_string($con,$param);
	}

	function ShowErrors(){
		error_reporting(E_ALL);
		ini_set('display_errors', 1);
	}
?>