<?php

$value1=$_POST["PosT"];
$value2=$_POST["GeT"];
echo "<div><label>".$value1."</div></label>";
echo "<div><label>".$value2."</div></label>";
?>

	<form action="../index.php">
		<input type="submit" value="Отмена"/>
	</form>



<!--
<form action="index.php" method="get" >
	<p>Вы ввели Post: <input type="text" id='userInput' name="userInput" /></p>
	<p>Вы ввели Get: <input type="text" id='userInput2' name="userInput2" /></p>
	 <p><input type="submit" value="Отмена"/></p>
</form>
-->
