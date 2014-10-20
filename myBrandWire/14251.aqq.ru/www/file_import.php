<?

$uploaddir = 'uploads/';
$uploadfile = $uploaddir . basename($_FILES['userfile']['name']);
$max_file_size = 300*1024*1024; // 300Mb

$do_action = isset($_REQUEST['do_action']) && !empty($_REQUEST['do_action']) ? $_REQUEST['do_action'] : '';

if ($do_action == 'load_file'){
	
	$user_id = isset($_REQUEST['user_id']) && !empty($_REQUEST['user_id']) ? $_REQUEST['user_id'] : '';
	
	if ($user_id != '') {
		
		/*$allowedExts = array("jpg", "jpeg", "gif", "png");*/
		$extension = end(explode(".", $_FILES["file"]["name"]));
		if (/*(($_FILES["file"]["type"] == "image/gif")
		    || ($_FILES["file"]["type"] == "image/jpeg")
		    || ($_FILES["file"]["type"] == "image/png")
		    || ($_FILES["file"]["type"] == "image/pjpeg"))
		    && (*/$_FILES["file"]["size"] < $max_file_size/*)
		    && in_array($extension, $allowedExts)*/)
		{
		    if ($_FILES["file"]["error"] > 0)
		    {
		        echo "Ошибка: " . $_FILES["file"]["error"] . "<br>";
		    }
		    else
		    {
		        echo "Загружаем файл: " . $_FILES["file"]["name"] . "<br>";
		        echo "Тип: " . $_FILES["file"]["type"] . "<br>";
		        echo "Размер: " . ($_FILES["file"]["size"] / 1024) . " kB<br>";
		        echo "Временный файл: " . $_FILES["file"]["tmp_name"] . "<br>";

		        if (file_exists($uploaddir . "id_" . $user_id . "_." . $extension))
		        {
		            echo "Файл " . $_FILES["file"]["name"] . " уже существует на сервере.";
		        }
		        else
		        {
		            move_uploaded_file($_FILES["file"]["tmp_name"],
		                $uploaddir . "id_" . $user_id . "_." . $extension);
		            echo "Загружен в: " . $uploaddir . "id_" . $user_id . "_." . $extension;
		        }
		    }
		}else{
		    echo "Размер файла больше 300Mb<br />";
		    //print_r($_FILES); 
		}
	
	}

}

?>