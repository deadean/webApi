			<div class="layer_0_copy-holder clearfix" >
				<div class="news clearfix" >
					<div class="c_wrapper15 clearfix ">
						<div class="group_3 clearfix">
							<div class="layer_1_copy_16-holder">
								<div class="layer_33-holder hrefWrapper clickable">
									<a href="mainNews/mainNews.php">Главные Новости</a>
								</div>
							</div>
							<div class="wrapper3 clearfix ">
								<?php
								if (is_file("ajaxPages/latestPaymentNews.php")) {
									include_once "ajaxPages/latestPaymentNews.php";
								}
								if (is_file("../ajaxPages/latestPaymentNews.php")) {
									include_once "../ajaxPages/latestPaymentNews.php";
								}
								?>
							</div>
						</div>

						<?php
						$addNewMainNews = "";

						if (!$user) {
							if (is_file("login/login.php")) {
								$addNewMainNews = "login/login.php";	
							}
							if (is_file("../login/login.php")) {
								$addNewMainNews = "../login/login.php";
							}
							
						} else {
							if (is_file("company/news/newsAdaptee.php")) {
								$addNewMainNews = "company/news/newsAdaptee.php";	
							}
							if (is_file("../company/news/newsAdaptee.php")) {
								$addNewMainNews = "../company/news/newsAdaptee.php";
							}
						}
						?>
						<form action="<?php echo $addNewMainNews; ?>">
							<div class="blockRelative blockLeft" style="top:5;">
								<div class="wrapper_hover2 " >
									<input type="submit" style="width:200" value="Добавить главную новость" class="commonTextStyle btnSeeNews2 borderNone clickable boldText"/>
								</div>
							</div>
						</form>
					</div>

					<div class="c_wrapper16 clearfix ">

						<div class="layer_1_copy_12-holder">
							<div class="layer_34-holder hrefWrapper clickable">
								<a href="freeNews/freeNews.php">Новости</a>
							</div>
						</div>

						<?php
							if (is_file("ajaxPages/freeNews.php")) {
								include_once 'ajaxPages/freeNews.php';	
							}
							if (is_file("../ajaxPages/freeNews.php")) {
								include_once '../ajaxPages/freeNews.php';
							} 
						?>

						<form action="
							<?php
							if (is_file("freeNews/freeNews.php")) {
								echo 'freeNews/freeNews.php';
							}
							if (is_file("../freeNews/freeNews.php")) {
								echo '../freeNews/freeNews.php';
							}
							
							?>">
							<div class="layer_1_copy_10-holder-holder" style="">
								<div class="wrapper_hover2 blockLeft" style="position: relative;top:10">
									<input type="submit" value="Все новости" class="commonTextStyle btnSeeNews2 borderNone clickable boldText"/>
								</div>
							</div>
						</form>
					</div>
				</div>

				<?php
					if (is_file("footer/footerMain.php")) {
						include_once 'footer/footerMain.php';
					}
					if (is_file("../footer/footerMain.php")) {
						include_once '../footer/footerMain.php';
					}
				
				?>
			</div>

			<div class="group_6 clearfix ">
				<div class="group_1 clearfix">
					<div class="c_wrapper10 clearfix">
						<a href="services/services.php?type=writing"><img class=" layer_22" src="images/layer_22.png" alt="" width="54" height="45" /></a>
						<p class=" publikaciya_na_caite">
							<strong>Поддержка в соц. сетях</strong>
					</div>
					<p class=" lorem_ipsum_dolor_sit_ame_2 commonTextFont107">
						Теперь, когда вы опубликуете новость у нас, она автоматически появится во всех указанных вами социальных сетях.
						Мы организовываем поддержку таких платформ : Facebook, Twitter, LiveJournal, Вконтакте и Одноклассники.
						Экономьте время на размещениии своих материалов!
					</p>
					<?php
						$toServices = "";

						if (!$user) {
							$toServices = "login/login.php";
						} else {
							$toServices = "services/services.php";
						}
						?>

						<form action="<?php echo $toServices; ?>">
							<div class="blockRelative blockLeft" style="top:5;">
								<div class="wrapper_hover2 " >
									<input type="submit" style="width:180" 
									value="Организовать поддержку" class="commonTextStyle btnSeeNews2 borderNone clickable boldText"/>
								</div>
							</div>
							<input type="hidden" name="type" value="social" />
						</form>
				</div>
				<div class="c_wrapper4">
					<div class="group_1_copy clearfix">
						<div class="c_wrapper12 clearfix">
							<a href="services/services.php?type=publish" ><img class=" layer_20" src="images/layer_20.png" alt="" width="52" height="54" /></a>
							<p class=" rasprostranenie_po_baze_s">
								<strong>Публикация в СМИ</strong>
								<!--
								<br style="margin-bottom: 0.0; line-height: 11.0px; display: block; content: ' ';" />
																<strong>Ð±Ð°Ð·Ðµ Ð¡ÐœÐ˜</strong>-->
								
							</p>
						</div>
						<p class=" lorem_ipsum_dolor_sit_ame_3 commonTextFont107">
							Наша политика, это подбор трастовых ресурсов с наличием гиперссылок.
							Мы не размещаем новость там где от ссылки не будет никакого эффекта.
							Таким образом мы заботимся о качестве и экономим деньги наших клиентов.
						</p>
						<?php
						$publishMainNews = "";

						if (!$user) {
							$publishMainNews = "login/login.php";
						} else {
							$publishMainNews = "company/news/newsAdaptee.php";
						}
						?>

						<form action="<?php echo $publishMainNews; ?>">
							<div class="blockRelative blockLeft" style="top:5;">
								<div class="wrapper_hover2 " >
									<input type="submit" style="width:150" value="Публиковать в СМИ" class="commonTextStyle btnSeeNews2 borderNone clickable boldText"/>
								</div>
							</div>
						</form>
					</div>
					<div class="group_1_copy_2 clearfix">
						<div class="c_wrapper30 clearfix">
							<a href="services/services.php?type=social"><img class=" layer_21" src="images/layer_21.png" alt="" width="54" height="54" /></a>
							<p class=" napisanie_press-reliza" style="width: 200">
								<strong>Написание</strong>
								<br style="margin-bottom: 0.0; line-height: 11.0px; display: block; content: ' ';" />
								<strong>качественного ПР</strong>
						</div>
						<p class=" lorem_ipsum_dolor_sit_ame_4 commonTextFont107">
							Мы умеем писать качественные и эффективные тексты на заданную тему.
							Все, что вам нужно, это ответить на несколько вопросов касающихся вашего мероприятия и одобрить заказ после его получения.
							Экономьте время на написание целой новости!
						</p>
						<form action="news/spellingNews.php">
							<div class="layer_1_copy_9-holder" style="">
								<div class="wrapper_hover2 " style="position: relative;top:-8">
									<input type="submit" value="Написать материал" class="commonTextStyle btnSeeNews2 borderNone clickable boldText"/>
								</div>
							</div>
						</form>
					</div>
				</div>
			</div>

			<div class="slide_show clearfix ">
                <div class="" style="width: 50;float: right">
                    <div class="blockPadding">
                        <a target="_blank" href="../lib/php/rss1/rss.php">
                            <img class=" " src="../images/mainRSS.png" alt="" width="25" height="25" />
                        </a>
                    </div>
                    <div>
                        <a  target="_blank" href="http://www.twitter.com">
                            <img class="hoverIcon " src="../images/mainTwitter.png" alt="" width="25" height="25" />
                        </a>
                    </div>
                    <div class="blockPadding">
                        <a target="_blank" href="https://www.facebook.com/groups/1411118389128924/">
                            <img class=" " src="../images/mainFacebook.png" alt="" width="25" height="25" />
                        </a>
                    </div>
                    <div>
                        <a target="_blank" href="https://vk.com/mybrandwire">
                            <img class=" " src="../images/mainVK.png" alt="" width="25" height="25" />
                        </a>
                    </div>
                </div>
				<div id="slideshow" class="c_wrapper5 clearfix ">
					<div class="slide-text clearfix" style="display: block;" id="slide1" >
						<div id="yt_player" class="layer_35"></div>
						<script language="javascript">
							/*document.addEventListener("DOMContentLoaded", init, false);
							function init() {
								var _video = document.getElementById("video");
								_video.addEventListener("playing", play_clicked, false);
								_video.addEventListener("pause", pause_clicked, false);
							}*/
							
							
							function play_clicked() {
								/*PauseSlide();*/
								$('#slideshow').cycle('pause');
							}

							function pause_clicked() {
								//ResumeSlide();
								$('#slideshow').cycle('resume');
							}
							
							
							var tag = document.createElement('script');
					        tag.src = "http://www.youtube.com/player_api";
					        var firstScriptTag = document.getElementsByTagName('script')[0];
					        firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

							var player;
					        function onYouTubePlayerAPIReady() {
					                player = new YT.Player('yt_player',
					                {
					                        height: '285',
					                        width: '420',
					                        videoId: 'f22JLBGhiTw',
					                        playerVars: { 'autoplay': 0, 'rel': 0 },
					                        events: {
					                                'onReady': onPlayerReady,
					                                'onStateChange': onPlayerStateChange
					                        }
					                });
					        }
					        function onPlayerReady(event) {
					        /// event.target.playVideo();
					        }
					        function onPlayerStateChange(event) {
					                if (event.data ==YT.PlayerState.PLAYING){
					                	/*_gaq.push(['_trackEvent', 'Page Name', 'Video Name', 'Play', player.getVideoUrl() ]);*/
					                	play_clicked();
									}
					                if (event.data ==YT.PlayerState.PAUSED){
					                	/*_gaq.push(['_trackEvent', 'Page Name', 'Video Name', 'Pause', player.getVideoUrl() ]);*/
					                	pause_clicked();
					                }
					                if (event.data ==YT.PlayerState.ENDED){
					                	/*_gaq.push(['_trackEvent', 'Page Name', 'Video Name', 'Watch to End', player.getVideoUrl() ]);*/
					                	pause_clicked();
					                } 
							}
							
							
							
						</script>
						<!--
						<video id='video' class="layer_35" controls
													<source id='mp4' src="resources/Main_3.mp4" type='video/mp4'>
													<p>Your browser does not support the video tag.</p>
												</video>-->
												
						<!--<iframe width="420" height="285" class="layer_35" src="//www.youtube.com/embed/f22JLBGhiTw?&rel=0" frameborder="0" allowfullscreen></iframe>-->
						
						<div class="c_wrapper28">
							<img class=" layer_38" src="images/layer_38.jpg" alt="" width="32" height="31" />
							<img class=" layer_38_copy" src="images/layer_38_copy.png" alt="" width="32" height="32" />
							<img class=" layer_38_copy_2" src="images/layer_38.jpg" alt="" width="32" height="31" />
							<img class=" layer_38_copy_3" src="images/layer_38.jpg" alt="" width="32" height="31" />
						</div>
						<div class="c_wrapper29 clearfix">
							<p class=" povysit_uroven_prodazh">
								<strong>Повысить Уровень Продаж</strong>
							</p>
							<p class=" sozdat_polozhitelnyi_imid">
								<strong>Создать Положительный Имидж</strong>
							</p>
							<p class=" vyvesti_v_tor_pozicii_poi">
								<strong>Вывести в ТОР Позиции Поисковой Выдачи</strong>
							</p>
							<p class=" sdelat_vash_brend_uznavae">
								<strong>Сделать Ваш Бренд Узнаваемым</strong>
							</p>
							<div class="c_wrapper38">
								<img class=" layer_39" src="images/layer_39.jpg" alt="" width="180" height="22" />
								<p class=" vse_eto_vozmozhno_s_brand">
									<em>Все это возможно с &nbsp;</em>
									<strong><em style="font-family: 'Trebuchet MS', Helvetica, sans-serif; color: #ffffff;">MyBrandWire.com</em></strong>
								</p>
							</div>
						</div>
						<?php
						$addNewMainNews = "";

						if (!$user) {
							$addNewMainNews = "login/login.php";
						} else {
							$addNewMainNews = "company/news/newsAdaptee.php";
						}
						?>
						<form action="<?php echo $addNewMainNews; ?>">
							<div class="blockRelative blockLeft" style="top:20;left:43">
								<div class="wrapper_hover2" >
									<input type="submit" style="width:150;height: 44;font-size: larger;" value="Начать работу" class="commonTextStyle btnSeeNews3 borderNone clickable boldText"/>
								</div>
							</div>
						</form>
					</div>
					<div class="slide-text" style="display: none;" id="slide2">
						<img src="images/slide2.jpg" alt="" class="slidehalf">
						<form action="<?php echo $addNewMainNews; ?>">
							<div class="blockRelative blockLeft" style="margin-top:-90;left:43">
								<div class="wrapper_hover2" >
									<input type="submit" style="width:150;height: 44;font-size: larger;" value="Начать работу" class="commonTextStyle btnSeeNews3 borderNone clickable boldText"/>
								</div>
							</div>
						</form>
					</div>
					<div class="slide-text" style="display: none;" id="slide3">
						
						<img src="images/slide3.jpg" alt="" class="slidehalf">
						<form action="<?php echo $toServices; ?>">
							<div class="blockRelative blockLeft" style="margin-top:-110;left:43;">
								<div class="wrapper_hover2" >
									<input type="submit" style="width:230;height: 46;font-size: larger;" 
									value="Организовать поддержку" class="commonTextStyle btnSeeNews3 borderNone clickable boldText"/>
								</div>
							</div>
							<input type="hidden" name="type" value="social" />
						</form>

					</div>
					<div class="slide-text" style="display: none;" id="slide4">
						<?php
						$toCompany = "";

						if (!$user) {
							$toCompany = "login/login.php";
						} else {
							$toCompany = "company/company.php";
						}
						?>

						<img src="images/slide4.jpg" alt="" class="slidehalf">
						<form action="<?php echo $toCompany; ?>">
							<div class="blockRelative blockLeft" style="margin-top:-100;left:42;">
								<div class="wrapper_hover2" >
									<input type="submit" style="width:200;height: 44;font-size: larger;" 
									value="Создать компанию" class="commonTextStyle btnSeeNews3 borderNone clickable boldText"/>
								</div>
							</div>
							<input type="hidden" name="type" value="autoAdd" />
						</form>

					</div>
				</div>
				<style>
				#nav1 div{
						width:13;
						height: 13;
						border-radius: 45;
						cursor: hand;
						background: Gainsboro;
						display: inline-block;
						margin-left: 2;
					}
					.el{
						width:13;
						height: 13;
						border-radius: 45;
					}
					.activeslide{
						background-color: SteelBlue!important;
					}
					
					
					
				</style>
				<script type="text/javascript" src="lib/jq/jquery-1.6.1.min.js"></script>
				<script type="text/javascript" src="js/jquery.cycle.all.v2.js"></script>
				
				<div class="c_wrapper6 clearfix container" id="nav1">
				</div>
				
			</div>
			<script>
					$('#slideshow').cycle({
						fx : 'fade',
						speed:    1200, 
    					timeout:  9000,
    					pager:  '#nav1'
					});
					
		    </script>