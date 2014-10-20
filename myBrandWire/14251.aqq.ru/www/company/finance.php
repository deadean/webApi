<?php session_save_path("../tmp");session_start();?>
<?php
  			include_once '../lib/csFunctions.php';
  			include_once '../model/model.php';
			
			$user = unserialize($_SESSION["userObject"]);
			Alert("dd");
			$idCompany = "";
			foreach($_GET as $key=>$val) {
				  $idCompany = $_GET[$key];
			}
			
			if(!$currentCompany)
	   			$currentCompany = $user->companies[$idCompany];
?>

<?php
	echo "dd";
	if(!is_null($currentCompany))
	{
		echo "Финансы";
	}
	echo "Финансы";

?>