<?php
	include_once '../model/controller.php';
	$idCountry = $_GET["param"];
	$controller = new Controller();
	$communities = $controller->GetCommunitiesByCountry($idCountry);
	//print_r($communities);
	echo "<select id='communitySelect'>";
	foreach ($communities as $key => $value) 
	{
		echo "<option value='$value->id'>".$value->name."</option>";	
	}
	echo "</select>";
?>
