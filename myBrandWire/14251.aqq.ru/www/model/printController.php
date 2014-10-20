<?php

	class PrintController{
		function PrintCompanyName($id){
			$user = unserialize($_SESSION["userObject"]);
			echo $user->companies[$id]->name;
		} 
	}
?>