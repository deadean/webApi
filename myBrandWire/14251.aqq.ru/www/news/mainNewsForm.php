<?php session_save_path("../tmp");session_start(); ?>
<?php
    include_once '../lib/csFunctions.php';
    InitGlobServerRedirectConstants();
    include_once "../model/controller.php";
    $controller = new Controller();
    $idNews = $_GET["id"];
    $currentNews = $controller -> GetNewsById($idNews);
    $news = $controller -> GetPaymentNews();
    $newsAction = "../mainNews/mainNews.php";
    if ($currentNews -> order -> payment == "0")
        $newsAction = "../freeNews/freeNews.php";

    $firstMainNewsImage = "";
    preg_match("#<img src=[\"\'](.+?)[\"\'](.*)/>#si", $currentNews -> content, $firstMainNewsImage);
    $firstMainNewsImage = $firstMainNewsImage[1];
?>

<html>
	<head>
		<meta http-equiv="content-type" content="text/html; charset=UTF-8" />
		<link rel="stylesheet" type="text/css" href="../company/style.css" />
		<link rel="stylesheet" type="text/css" href="../commonStyles.css" />
		<link rel="stylesheet" type="text/css" href="../header/style.css" />
        <link rel="shortcut icon" href="../images/favicon.ico">

        <link rel="stylesheet" type="text/css" href="style.css" />
		<script type="text/javascript" src="../services/servicesScript.js"></script>
		<script type="text/javascript" src="http://vk.com/js/api/share.js?90" charset="windows-1251"></script>
		<script type="text/javascript" src="https://apis.google.com/js/plusone.js">
			{ lang:'ru'
			}
		</script>

	</head>

	<body onload="AnaliseAndSetElementsVisisbility('<?php echo $_SESSION["loginUser"] ?>', '<?php echo $_SESSION["idUser"] ?>')">
		<div id="fb-root"></div>
		<script>
			( function(d, s, id) {
					var js, fjs = d.getElementsByTagName(s)[0];
					if (d.getElementById(id))
						return;
					js = d.createElement(s);
					js.id = id;
					js.src = "//connect.facebook.net/ru_RU/all.js#xfbml=1";
					fjs.parentNode.insertBefore(js, fjs);
				}(document, 'script', 'facebook-jssdk'));
		</script>
		<div class="global_container_ expandingBoxFix" style="min-height: 800;height:100%">

			<?php
			include_once '../header/headerMain.php';
			?>

			<div id="idMainContent">
			<div class=" emptyContent container " style="top:55;height: 100%">

					<?php
					include_once 'mainNewsFormMainContent.php';
					?>
			</div>

			</div>
		</div>

	</body>

</html>
