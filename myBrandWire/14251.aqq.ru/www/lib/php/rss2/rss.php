<?php
     include "rss.inc";
     include "../../../model/conn.inc";


   $Rss= new CRss();

   $Rss->Title="My BrandWire Rss";
   $Rss->Link="http://mybrandw.s66.r53.com.ua";
   $Rss->Copyright="MyBrandWire.";
   $Rss->Description="Description MyBrandWire";
   $Rss->Category = "News ";
   $Rss->Language="ru";

   $Rss->ManagingEditor="deadean@yandex.ru";
   $Rss->WebMaster="deadean@yandex.ru";
   /*$Rss->Query="SELECT
                BLOG.title,
                BLOG.description,
                BLOG.link,
                BLOG.date,
                BLOG.category
     FROM BLOG
     ORDER by DATE desc Limit 0,20";*/

   $Rss->Query="SELECT
                news.header,
                news.common,
                news.id,
                news.publish_time,
                news.tags
     FROM news
     ORDER by news.publish_time desc Limit 0,20";

    $Rss->Open($Server,$DataBase,$Login,$Password);
     $Rss->LastBuildDate=date("r");
     $query = "select news.publish_time
                        FROM news
          ORDER by news.publish_time desc Limit 0,1";

/*$query = "select BLOG.date
                        FROM BLOG
          ORDER by DATE desc Limit 0,1";*/

      $result1 = mysql_query($query)
              or die("FROM blog failed");

      $line = mysql_fetch_array($result1);

      $Date =date("r",strtotime($line[0]));
       mysql_free_result($result1);

      $Rss->LastBuildDate=$Date;
      $Rss->PubDate=$Rss->LastBuildDate;

     $Rss->PrintHeader();
     $Rss->Query();

     while ($line = mysql_fetch_array($Rss->Result))
     {
               $Title = $line[0];
               $Description = $line[1];
               $Link=$line[2];
               $PubDate=date("r",strtotime($line[3]));
               $Category=$line[4];
               $Rss->PrintBody($Title,$Link,$Description,$Category,$PubDate);
    }
    $Rss->PrintFooter();
    $Rss->Close();

?>









