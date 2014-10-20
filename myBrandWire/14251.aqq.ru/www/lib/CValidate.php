<?php session_start(); ?>
<?php
class CValidate {
    public static function ValidateHtmlString($string){
        return CValidate::ApplyParseHtmlString(strip_tags($string,"<img><a><b><i><strong>"));
    }

    private static function ApplyParseHtmlString($html_string){
        //$imgArray = explode('<img>', $html_string);
        //print_r($imgArray);
        $res="";
        $isImageBlock=0;
        //echo "inn".strlen($html_string);
        for($i=0;$i<=strlen($html_string);$i++){
            if(
                $html_string[$i]=='<' && $html_string[$i+1]=='i' &&$html_string[$i+2]=='m' &&
                $html_string[$i+3]=='g'
            )
            {
                $isImageBlock=1;
                $res=$res."<div style='margin:auto;width:80%'>";
            }
            $res=$res.$html_string[$i];
            if($html_string[$i]=='>' && $isImageBlock==1){
                $res=$res."</div>";
                $isImageBlock=0;
            }

        }
        //echo "out";
        //echo "</br>";
        return $res;
    }

	public static function isValidUrl($url){
		if(!$url || !is_string($url)){
			return false;
		}
		if( ! preg_match('/^http(s)?:\/\/[a-z0-9-]+(.[a-z0-9-]+)*(:[0-9]+)?(\/.*)?$/i', $url) ){
			return false;
		}

		$file_headers = @get_headers($url);
		if($file_headers[0] == 'HTTP/1.1 404 Not Found') {
			$exists = false;
		}
		else {
			$exists = true;
		}

		return true;
	}
}