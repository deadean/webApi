<!DOCTYPE html>
<html>
<head>
	<script src="../lib/tinymce/js/tinymce/tinymce.min.js"></script>
<script>
        tinymce.init({selector:'textarea'});
        function Hells(){
        	alert(document.getElementById("textAreaId").value);
        }
</script>
</head>
<body>
	<form method="post" action="../test.php">
    	<textarea name="content" style="width:100%"></textarea>
    	<input type="submit" />
	</form>
</body>
</html>