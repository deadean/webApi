<?php
/**
 * Created by PhpStorm.
 * User: dean
 * Date: 09.07.14
 * Time: 21:32
 */

namespace php\Interfaces\Recovery;

class CRecoveryBase extends \Base{
	public $idUser;
	public $sendTime;
	public $userEmail;
	public $inputHashCode;

	public function GetHashCode(){
		return md5($this->idUser.$this->userEmail);
	}

	public function Save(){
		$this->model->SaveRecoveryBase($this);
	}

	public function IsInputHashCodeExistAndUseful(){
		return $this->model->IsInputHashCodeExistAndUseful($this);
	}

	public function Remove(){
		return $this->model->RemoveEntity($this->idUser,\csConstants::$csRecoveryBase);
	}

} 