<?php

class SitesComponent
{
    public static function formReportContent($newSitesInfo)
    {
        $html = "<table>";
        foreach ($newSitesInfo as $site)
        {
            $html .= "<tr><td>";
            if ($site['logo']) 
            {
                $html .= "<img src='".$_SERVER['HTTP_HOST']."/client/images/logos/" . $site['logo'] . "'>";
            }
            $html .= "</td><td>";
            $html .= "<span>" . $site['name'] . "</span>";
            $html .= "</td><td>";
            if ($site['link'])
            {
                $html .= "<span>" . urldecode($site['link']) . "</span>";
                $html .= "</td><td>";
                $html .= "<a href='" . urldecode($site['link']) . "'>Go to new</a>";
            }
            $html .= "</td></tr>";
        }
        $html .= "</table>";
        return $html;
    }
}

?>
