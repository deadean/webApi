<?php ?>
<?php
/**
 * Created by PhpStorm.
 * User: dean
 * Date: 08.06.14
 * Time: 13:55
 */

include_once '../model/controller.php';
class CActionManager {
    private $model;
    public function __construct($_model) {$this->model = $_model;}

    public function DoActionByObject($action, $object){
        if($action == "UpdateInstance"){
            if($object->ClassType==csConstants::$csCOMPANY){
                $company = new Company();
                $company->id = $object->id;
                $company->about = $object->about;
                Model::getInstance()->UpdateCompany($company);
                echo json_encode($company);
            }
        }
    }
}