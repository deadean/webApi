<?php

if(!isValidUrl($file))
	return;

$file_headers = @get_headers($file);
if($file_headers[0] == 'HTTP/1.1 404 Not Found') {
	$exists = false;
	echo "NOT EXIST";
}
else {
	echo "EXIST";
	$exists = true;
}


?>

<link rel="stylesheet" type="text/css" href="style.css" />
<link rel="stylesheet" type="text/css" href="../commonStyles.css" />
<link rel="stylesheet" type="text/css" href="../header/style.css" />
<link rel="stylesheet" href="../css/normalize.css">
<link rel="stylesheet" href="../css/styleModalWindow.css" media="screen" type="text/css" />
<link rel="stylesheet" type="text/css" href="js/simptip-mini.css" media="screen,projection" />

<script type="text/javascript" src="../services/servicesScript.js"></script>
<script type="text/javascript" src="js/jqueryLib.js"></script>

<script type="text/javascript" src="../lib/jq/1.10.2/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../lib/jq/1.10.2/jquery.form.min.js"></script>
<script type='text/javascript' src='../lib/knockouts/knockout-3.1.0.js'></script>


<div>
	<form id="myForm" action="../services/services.php" method="post">
		Name: <input type="text" name="name" />
		Comment: <textarea name="comment"></textarea>
		<div onclick="$('#myForm').ajaxSubmit()">hello</div>
	</form>
</div>