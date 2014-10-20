<?php session_save_path("../tmp");session_start();?>
<?php
  			include_once '../lib/csFunctions.php';
  			include_once '../model/model.php';
			
			$user = unserialize($_SESSION["userObject"]);
			$idCompany = "";
			foreach($_GET as $key=>$val) {
				  $idCompany = $_GET[$key];
			}
			
			if(!$currentCompany)
	   			$currentCompany = $user->companies[$idCompany];
?>

<?php
	if(!is_null($currentCompany))
	{
		echo "Товары";
	}
?>