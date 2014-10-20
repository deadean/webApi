<?php
    include_once 'csConstants.php';

	class Company extends Base implements  IEntity{
		public $id;
		public $name;
		public $about;
		public $region;
		public $community;
		public $persons;
		public $logo;

        public function __construct() {
            parent::__construct();
            $this->ClassType = csConstants::$csCOMPANY;
        }

		public function RefrreshPersons(){
			$this->persons = $this->model->GetPersonsByCompany($this->id);
		}

        public function RefreshCompanyInfo(){
            $this->name = $this->model->GetCompanyInfo($this->id)->name;
            $this->about = $this->model->GetCompanyInfo($this->id)->about;
            $this->logo = $this->model->GetCompanyInfo($this->id)->logo;
        }

		public $news;
		public function RefreshNews(){
			$this->news = $this->model->GetNewsByCompany($this->id);
		}
		
		public $communications;
		public function RefrreshCommunications(){
			$this->communications = $this->model->GetCommunicationsByCompanyId($this->id);
			$this->region = $this->model->GetRegionByCompanyId($this->id);
			$this->community = $this->model->GetCommunityByCompanyId($this->id);
		}

        public function IsSocialNetworksExist(){
            foreach($this->communications as $item){
                if($item->name=='Facebook' || $item->name=='Vkontakte' || $item->name=='Googleplus'
                    || $item->name=='Twitter' || $item->name=='Odnoklassniki' || $item->namw == 'Instagramm'
                    || $item->name=='Pinterest' || $item->name=='Tumblr' || $item->namw == 'Linkedin'
                )
                {
                    return 1;
                }
            }
            return 0;
        }

        public function Save()
        {
            $this->model->UpdateCompany($this);
        }
    };
	
	class Person{
		public $id;
		public $firstname;
		public $secondname;
		public $middlename;
		public $position;
		public $communications;
	};
	
	class CommunicationType{
		public $id;
		public $name;
		public $value;

        public function IsNetworkTypes($type){
            $excludedNetworkTypes = array("Email","Skype","Phone");
            return in_array($type,$excludedNetworkTypes);
        }
	};
	
	class User extends Base{
		
		public $id;
		public $login;
		public $password;
		
		public $companies;
		public function RefrreshCompanies()
		{
			$this->companies = $this->model->GetCompaniesByUser($this->id);
		}
		
	};
	
	class News extends Base{
		public $id;
		public $header;
		public $common;
		public $content;
		public $tags;
		public $company;
		public $categories;
		public $order;
		public $comments;
		public $isBlog;
        public $status;
		public function Save(){
			$this->model->UpdateNews($this);
		}
	};
	
	class Order extends Base{
		public $id;
		public $news;
		public $company;
		public $payment;
		public $isPublish;
        public $isAutoAddCompanySocialNetworks;
        public $isAutoAddAboutCompanyInfo;
        public $isAutoAddCompanyAddress;
        public $isAutoAddCompanyContacts;
		public $datePublish;
        public $totalCost;
        public $contactsIds;

	};
	
	class Category{
		public $id;
		public $name;
	}
	
	class Country{
		public $id;
		public $name;
		public $communities;
	}
	
	class Community{
		public $id;
		public $name;
		public $idCountry;
	}
	
	class Email extends Base{
		public $To;
		public $From;
		public $Subject;
		public $Message;
		public function Send(){
			$this->model->SendEmailToUs($this);
		}
	}

?>