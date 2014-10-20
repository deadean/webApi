<?php
if (!$_SESSION)
    session_start();
?>
<?php

	if(is_file('../lib/php/Errors/enErrors.php'))
		include '../lib/php/Errors/enErrors.php';
	if(is_file('lib/php/Errors/enErrors.php'))
		include 'lib/php/Errors/enErrors.php';

class Model
{
    private $hostname = "localhost";

    private $username = "root";
    private $password = "";
    private $dbName = "aqq14251_mybrandwire";

//    private $username = "mybrandw_root";
//                private $password = "mybrandwire";
//                private $dbName = "mybrandw_mybrandwire";

    private $userstable = "users";
	private $user_recovery="user_recovery";
    private $personTable = "person";
    private $pressrealise = "news";
    private $orders = "orders";
    private $companyTable = "company";
    private $company_PersonTable = "company_person";
    private $categoryTable = "category";
    private $news_categoryTable = "news_category";
    private $countryTable = "country";
    private $communitiesTable = "city";
    private $company_social_networksTable = "company_social_networks";
    private $person_communicationTable = "person_communication";
    private $communication_typeTable = "communication_type";
    private $order_contactsTable = "orders_contact";

	private $_company;

    protected static $_instance;

    public function __construct()
    {
    }

    private function __clone()
    {
    }

    public static function getInstance()
    {

        if (null === self::$_instance) {
            //Alert("new Controller");
            self::$_instance = new self();
            //PrintStackInclue();
        }
        return self::$_instance;
    }

    public function GetConnection()
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");
        return $con;
    }

	public function CloseConnection($con){
		mysqli_close($con);
	}

    public function GetCommunicationsByPersonId($idPerson)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $table_ = $this->userstable;
        $query = "SELECT * FROM communication_type, person_communication
                    WHERE communication_type.id = person_communication.idCommunicationType
                          AND person_communication.idPerson = '$idPerson'";

        //echo $query;

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $item = new CommunicationType();
            $item->id = $row['id'];
            $item->name = $row['name'];
            $item->value = $row['communicationValue'];
            $result[$item->id] = $item;
        }

        mysqli_close($con);
        return $result;
    }

    public function GetCommunications()
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "SELECT * FROM $this->communication_typeTable";


        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $item = new CommunicationType();
            $item->id = $row['id'];
            $item->name = $row['name'];
            $result[$item->id] = $item;
        }

        mysqli_close($con);
        return $result;
    }

    public function GetRegionByCompanyId($idCompany)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");
        $query =
            "
            SELECT * FROM $this->companyTable
              WHERE $this->companyTable.id = $idCompany
        ";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = "";
        while ($row = mysqli_fetch_array($res)) {
            $result = $row["region"];
        }

        mysqli_close($con);
        return $result;
    }

    public function GetCommunityByCompanyId($idCompany)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");
        $query =
            "
            SELECT * FROM $this->companyTable WHERE $this->companyTable.id = $idCompany
        ";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = "";
        while ($row = mysqli_fetch_array($res)) {
            $result = $row["community"];
        }

        mysqli_close($con);
        return $result;
    }

    public function GetCommunicationsByCompanyId($idCompany)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $table_ = $this->userstable;
        $query = "
                    SELECT * FROM $this->communication_typeTable, $this->company_social_networksTable
                        WHERE $this->communication_typeTable.id = $this->company_social_networksTable.idCommunicationType
                              AND $this->company_social_networksTable.idCompany = '$idCompany'";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $item = new CommunicationType();
            $item->id = $row['id'];
            $item->name = $row['name'];
            $item->value = $row['communicationValue'];
            $result[$item->id] = $item;
        }

        mysqli_close($con);
        return $result;
    }

    public function GetPersonsByCompany($idCompany)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "SELECT * FROM company_person, person WHERE company_person.idPerson = person.id AND company_person.idCompany = '$idCompany'";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $item = new Person();
            $item->id = $row['id'];
            $item->firstname = $row['firstname'];
            $item->secondname = $row['secondname'];
            $item->middlename = $row['middlename'];
            $item->position = $row['position'];
            $item->communications = $this->GetCommunicationsByPersonId($item->id);
            $result[$item->id] = $item;
        }

        mysqli_close($con);
        return $result;
    }

    public function GetCompaniesByNews($idNews)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "
            SELECT * FROM company WHERE company.id IN
            (
                SELECT idCompany as id FROM news,orders
                    WHERE
                        news.idOrder = orders.id
                    AND
                        news.id = '$idNews'
            )
        ";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = new Company();
        while ($row = mysqli_fetch_array($res)) {
            $result = new Company();
            $result->id = $row['id'];
            $result->name = $row['name'];
            $result->logo = $row['logo'];
            $result->communications = $this->GetCommunicationsByCompanyId($result->id);
            break;
        }

        mysqli_close($con);
        return $result;
    }

    public function GetCompaniesByUser($idUser)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "SELECT * FROM $this->companyTable WHERE $this->companyTable.idUser = '$idUser'";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $company = new Company();
            $company->id = $row['id'];
            $company->name = $row['name'];
            $company->about = $row['about'];
            $company->logo = $row['logo'];
            $company->region = $row['region'];
            $company->community = $row['community'];
            $company->persons = $this->GetPersonsByCompany($company->id);
            $company->news = $this->GetNewsByCompany($company->id);
            $company->communications = $this->GetCommunicationsByCompanyId($company->id);
            $result[$company->id] = $company;
        }

		$this->_company=$company;

        mysqli_close($con);
        return $result;
    }

    public function GetCompanyById($idCompany)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "SELECT * FROM $this->companyTable WHERE $this->companyTable.id = '$idCompany'";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = new Company();
        while ($row = mysqli_fetch_array($res)) {
            $company = new Company();
            $company->id = $row['id'];
            $company->name = $row['name'];
            $company->about = $row['about'];
            $company->logo = $row['logo'];
            $company->region = $row['region'];
            $company->community = $row['community'];
            $company->persons = $this->GetPersonsByCompany($company->id);
            $company->news = $this->GetModeratedNewsByCompany($company->id);
            $company->communications = $this->GetCommunicationsByCompanyId($company->id);
            $result = $company;
        }

        mysqli_close($con);
		$this->_company = $result;
        return $result;
    }

    public function GetCompanyInfo($idCompany)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "SELECT * FROM $this->companyTable WHERE $this->companyTable.id = '$idCompany'";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = new Company();
        while ($row = mysqli_fetch_array($res)) {
            $company = new Company();
            $company->id = $row['id'];
            $company->name = $row['name'];
            $company->about = $row['about'];
            $company->logo = $row['logo'];
            $company->region = $row['region'];
            $company->community = $row['community'];
            $result = $company;
        }

        mysqli_close($con);
		$this->_company = $company;

        return $result;
    }

	public function  GetCompanyByCashe($idCompany){
		return $this->_company != NULL && $this->_company->id == $idCompany ? $this->_company:
			$this->GetCompanyById($idCompany);
	}

    public function GetNewsByCompany($idCompany)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "
            SELECT news.id as newsId, orders.id as ordersId, news.*, orders.* FROM $this->pressrealise inner join orders on
                $this->orders.idCompany = '$idCompany' and
                $this->orders.id = $this->pressrealise.idOrder
                and $this->pressrealise.isCompanyOwner = 1
                and $this->pressrealise.isBlog = 0
            ORDER BY $this->pressrealise.id DESC
        ";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $item = new News();
            $item->id = $row['newsId'];
            $item = $this->GetNewsById($item->id);
            /*			$item -> header = $row['header'];

                        $order = new Order();
                        $order -> id = $row['ordersId'];
                        $order -> payment = $row['payment'];
                        $order -> isPublish = $row['isPublish'];
                        $item -> order = $order;
            */

            $result[$item->id] = $item;
        }

        mysqli_close($con);
        return $result;
    }

    public function GetModeratedNewsByCompany($idCompany)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "
            SELECT news.id as newsId, orders.id as ordersId, news.*, orders.* FROM $this->pressrealise inner join orders on
                $this->orders.isPublish = '1'
                and $this->orders.idCompany = '$idCompany' and
                $this->orders.id = $this->pressrealise.idOrder
                and $this->pressrealise.isCompanyOwner = 1
                and $this->pressrealise.isBlog = 0
                and ($this->pressrealise.status = 'moderated' OR $this->pressrealise.status = 'editPublishedNews')
            ORDER BY $this->pressrealise.id DESC
        ";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $item = new News();
            $item->id = $row['newsId'];
            $item = $this->GetNewsById($item->id);
            /*			$item -> header = $row['header'];

                        $order = new Order();
                        $order -> id = $row['ordersId'];
                        $order -> payment = $row['payment'];
                        $order -> isPublish = $row['isPublish'];
                        $item -> order = $order;
            */

            $result[$item->id] = $item;
        }

        mysqli_close($con);
        return $result;
    }

    public function GetCategories()
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $table_ = $this->categoryTable;
        $query = "SELECT * FROM $table_";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $item = new Category();
            $item->id = $row['id'];
            $item->name = $row['name'];
            $result[$item->id] = $item;
        }

        mysqli_close($con);
        return $result;
    }

    public function GetPaymentNews()
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "SELECT * FROM " . $this->pressrealise . " WHERE idOrder IN
            (
                SELECT id as idOrder FROM " . $this->orders
					. " WHERE payment LIKE '1'
							and isPublish='1'
							and datePublish < NOW()
						ORDER BY $this->orders.datePublish
            )
            AND isBlog='0' and (status = 'moderated' OR status = 'editPublishedNews')
		ORDER BY $this->pressrealise.id DESC
		";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $id = $row['id'];
            $item = $this->GetNewsById($id);
            if(empty($item->id))
                continue;
            $item->company = $this->GetCompaniesByNews($item->id);
            $result[$item->id] = $item;
        }

        mysqli_close($con);
        return $result;
    }

    public function GetPaymentNewsByCategoryId($idCategory)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "SELECT
					$this->news_categoryTable.id as idNewsCategoryTable,
					$this->news_categoryTable.idNews as idNews,
					$this->news_categoryTable.idCategory as idCategory,
					$this->pressrealise.* FROM " . $this->pressrealise . ", $this->news_categoryTable" . " WHERE idOrder IN
					(
						SELECT id as idOrder FROM " . $this->orders
							. " WHERE payment LIKE '1'
								AND isPublish='1'
								AND datePublish < NOW()
								ORDER BY $this->orders.datePublish
					)
					AND isBlog='0'
					AND (status = 'moderated' OR status = 'editPublishedNews')
					AND $this->news_categoryTable.idNews = $this->pressrealise.id
					AND $this->news_categoryTable.idCategory = '$idCategory'
		ORDER BY $this->pressrealise.id DESC
		";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $id = $row['id'];
            $item = $this->GetNewsById($id);
            $item->company = $this->GetCompaniesByNews($item->id);
            $result[$item->id] = $item;
        }

        mysqli_close($con);
        return $result;
    }

    public function GetFreeNewsByCategoryId($idCategory)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "SELECT
					$this->news_categoryTable.id as idNewsCategoryTable,
					$this->news_categoryTable.idNews as idNews,
					$this->news_categoryTable.idCategory as idCategory,
					$this->pressrealise.* FROM " . $this->pressrealise . ",
					$this->news_categoryTable" . " WHERE idOrder IN
					(
						SELECT id as idOrder FROM "
                        . $this->orders
                        . " WHERE payment LIKE '0'
                        and isPublish='1'
                        and datePublish < NOW()
                        ORDER BY $this->orders.datePublish
					)
					AND isBlog='0'
					AND (status = 'moderated' OR status = 'editPublishedNews')
					AND $this->news_categoryTable.idNews = $this->pressrealise.id
					AND $this->news_categoryTable.idCategory = '$idCategory'
		ORDER BY $this->pressrealise.id DESC
		";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $id = $row['id'];
            $item = $this->GetNewsById($id);
            $item->company = $this->GetCompaniesByNews($item->id);
            $result[$item->id] = $item;
        }

        mysqli_close($con);
        return $result;
    }

    public function GetFreeNews()
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "SELECT * FROM " . $this->pressrealise
            . " WHERE idOrder IN (SELECT id as idOrder FROM "
            . $this->orders . " WHERE payment LIKE '0' and isPublish='1' and datePublish < NOW()) AND isBlog='0'
            and (status='moderated' OR status='editPublishedNews')
		ORDER BY $this->pressrealise.id DESC
		";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $item = new News();
            $id = $row['id'];
            $item = $this->GetNewsById($id);
            $item->company = $this->GetCompaniesByNews($item->id);
            $result[$item->id] = $item;

            $result[$item->id] = $item;
        }

        mysqli_close($con);
        return $result;
    }

    public function GetCategoriesByNews($id)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "SELECT * FROM $this->news_categoryTable, $this->categoryTable
					WHERE $this->news_categoryTable.idNews = $id
					AND $this->news_categoryTable.idCategory = $this->categoryTable.id";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $item = new Category();
            $item->id = $row['idCategory'];
            $item->name = $row['name'];
            $result[$item->id] = $item;
        }

        mysqli_close($con);
        return $result;
    }

    public function GetOrderContactsByOrderId($id)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");


        $query = "SELECT * FROM $this->company_PersonTable WHERE $this->company_PersonTable.id IN(
            SELECT $this->order_contactsTable.idContact FROM $this->order_contactsTable WHERE $this->order_contactsTable.idOrder = '$id')
        ";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $id = $row['idPerson'];
            $result[$id] = $id;
        }

        mysqli_close($con);
        return $result;
    }

    public function GetNewsById($id)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "SELECT news.id as newsId, orders.id as ordersId, news.*, orders.* FROM $this->pressrealise inner join orders on
				$this->orders.id = $this->pressrealise.idOrder
				and $this->pressrealise.isBlog = 0
				and $this->pressrealise.id=$id";
        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $item = new News();
        while ($row = mysqli_fetch_array($res)) {
            $item->id = $row['newsId'];
            $item->header = $row['header'];
            $item->content = $row['content'];
            $item->common = $row['common'];
            $item->tags = $row['tags'];
            $item->comments = $row['comments'];
            $item->status = $row['status'];
            $item->categories = $this->GetCategoriesByNews($id);

            $order = new Order();
            $order->id = $row['ordersId'];
            $order->payment = $row['payment'];
            $order->isPublish = $row['isPublish'];
            $order->datePublish = $row['datePublish'];
            $order->isAutoAddAboutCompanyInfo = $row["isAutoAddAboutCompanyInfo"];
            $order->isAutoAddCompanyAddress = $row["isAutoAddCompanyAddress"];
            $order->isAutoAddCompanyContacts = $row["isAutoAddCompanyContacts"];
            $order->isAutoAddCompanySocialNetworks = $row["isAutoAddCompanySocialNetworks"];
            $order->contactsIds = $this->GetOrderContactsByOrderId($order->id);
            $item->order = $order;

            $item->company = $this->GetCompaniesByNews($item->id);
            break;
        }

        mysqli_close($con);
        return $item;
    }

    public function GetBlogNews()
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "SELECT * FROM " . $this->pressrealise . " WHERE idOrder IN (SELECT id as idOrder FROM " . $this->orders . " WHERE payment LIKE '1') AND isBlog='1'";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $item = new News();
            $item->id = $row['id'];
            $item->header = $row['header'];
            $item->content = $row['content'];
            $item->company = $this->GetCompaniesByNews($item->id);

            $result[$item->id] = $item;
        }

        mysqli_close($con);
        return $result;
    }

    public function GetCountries()
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");
        $query = "SELECT * FROM $this->countryTable";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $item = new Country();
            $item->id = $row['country_id'];
            $item->name = $row['name'];
            //echo $item->name;
            $item->communities = $this->GetCommunitiesByCountry($item->id);
            $result[$item->id] = $item;
        }

        mysqli_close($con);
        return $result;
    }

    public function GetCommunitiesByCountry($idCountry)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $table_ = $this->communitiesTable;
        $query = "SELECT * FROM $table_ WHERE $table_.country_id=$idCountry";

        //echo $query;

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $item = new Community();
            $item->id = $row['city_id'];
            $item->name = $row['name'];
            $item->idCountry = $row['country_id'];
            $result[$item->id] = $item;
        }

        mysqli_close($con);
        return $result;
    }

    public function AddNewUser($nameUser, $surnameUser, $emailUser, $phoneUser, $login, $password)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $table_ = $this->userstable;
        $password = md5($password);
        $query = "INSERT INTO $table_ VALUES(NULL,'$login','$password','$nameUser','$surnameUser','$emailUser','$phoneUser','0')";
        mysqli_query($con, $query) or die(mysqli_error($con));

        $query = "SELECT MAX(id) as 'Res' FROM $table_";
        $res = mysqli_query($con, $query);

        $row = mysqli_fetch_row($res);

        mysqli_close($con);
        return $row[0];
    }

    public function AddNewCompany($name, $logo, $about, $region, $community, $userId)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $table_ = $this->companyTable;
        $query = "INSERT INTO $table_ VALUES(NULL,'$name','$logo','$about','$region','$community','$userId')";
        mysqli_query($con, $query) or die(mysqli_error($con));

        $query = "SELECT MAX(id) as 'Res' FROM $this->companyTable";
        $res = mysqli_query($con, $query);
        $row = mysqli_fetch_row($res);

		$uploaddir = '../uploads/';
		$uploadfile = $uploaddir . "id_" . $row[0] . "_" . ".jpg";

		$table_ = $this->companyTable;
		$query = "UPDATE $table_ SET logo = '$uploadfile' WHERE id='$row[0]'";
		mysqli_query($con, $query) or die(mysqli_error($con));

        mysqli_close($con);
        return $row[0];
    }

    public function AddOrder($order)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $table_ = $this->orders;
        $query = "SELECT MAX(id) as 'Res' FROM $table_";
        $res = mysqli_query($con, $query);
        $row = mysqli_fetch_row($res);
        $order->id = $row[0];
        $order->id = $order->id + 1;

        $table_ = $this->orders;
        $idCompany = $order->company->id;
        $isAddContact = is_null($order->contactsIds) ? 0 : 1;
        $query = "INSERT INTO $table_
		    VALUES('$order->id','1','$order->payment','$idCompany','$order->payment','$order->datePublish','$order->isAutoAddCompanySocialNetworks','$isAddContact','$order->isAutoAddAboutCompanyInfo','$order->isAutoAddCompanyAddress','$order->totalCost','1')";
        //echo $query;
        mysqli_query($con, $query) or die(mysqli_error($con));

        $table_ = $this->pressrealise;
        $news = $order->news;
        $query =
            "INSERT INTO $table_ VALUES(
            NULL,
            '$news->header',
            '$news->common',
            '$news->content',
            '$news->tags',
            '$news->comments',
            '$order->id','$news->status','0','1'
            ,'$order->datePublish'
            ,'0'
            )";
        //echo $query;
        mysqli_query($con, $query) or die(mysqli_error($con));

        $table_ = $this->pressrealise;
        $query = "SELECT MAX(id) as 'Res' FROM $table_";
        $res = mysqli_query($con, $query);
        $row = mysqli_fetch_row($res);
        $news->id = $row[0];

        $table_ = $this->news_categoryTable;
        $news = $order->news;
        $query = "";
        if ($news->categories != NULL) {
            foreach ($news->categories as $key => $value) {
                $query = "INSERT INTO $table_ VALUES(NULL,'$value->id','$news->id')";
                mysqli_query($con, $query) or die(mysqli_error($con));
            }
        }

        if ($order->contactsIds != NULL) {
            foreach ($order->contactsIds as $key => $value) {
                $query = "SELECT id FROM $this->company_PersonTable WHERE $this->company_PersonTable.idPerson = '$value'";
                $res = mysqli_query($con, $query);
                $row = mysqli_fetch_row($res);
                $idContact = $row[0];
                $query = "INSERT INTO $this->order_contactsTable VALUES('$order->id','$idContact')";
                mysqli_query($con, $query) or die(mysqli_error($con));
            }
        }

        mysqli_close($con);
    }

    public function AddPersonContactByCompany($idCompany, $idContact, $name, $position, $email, $phone, $skype)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");
        mysqli_query($con, "SET AUTOCOMMIT=0");
        mysqli_query($con, "START TRANSACTION");

        $idContactNew = "";
        if ($idContact == "default") {
            $query = "INSERT INTO $this->personTable VALUES(NULL,'$name','','','$position')";
            mysqli_query($con, $query) or die(mysqli_error($con));

            $query = "SELECT MAX(id) as 'Res' FROM $this->personTable";
            $res = mysqli_query($con, $query);
            $row = mysqli_fetch_row($res);
            $idContactNew = $row[0];

            $query = "INSERT INTO $this->company_PersonTable VALUES(NULL,'$idContactNew','$idCompany')";
            mysqli_query($con, $query) or die(mysqli_error($con));
        }

        $idPerson = $idContactNew;

        if ($idContact != "default") {
            $query = "UPDATE $this->personTable SET $this->personTable.firstname = '$name' WHERE $this->personTable.id = '$idContact'";
            mysqli_query($con, $query) or die(mysqli_error($con));
            $query = "UPDATE $this->personTable SET $this->personTable.position = '$position' WHERE $this->personTable.id = '$idContact'";
            mysqli_query($con, $query) or die(mysqli_error($con));
        }

        if ($email != NULL) {
            $query = "SELECT id as idCommunicationType FROM $this->communication_typeTable WHERE name LIKE 'email'";
            $res = mysqli_query($con, $query);
            $idCommunicationType = "";
            while ($row = mysqli_fetch_array($res)) {
                $idCommunicationType = $row['idCommunicationType'];
                break;
            }
            $query = $idContact == "default"
                ? "INSERT INTO $this->person_communicationTable VALUES(NULL,'$idPerson','$idCommunicationType','$email')"
                : "UPDATE $this->person_communicationTable SET $this->person_communicationTable.communicationValue = '$email'
				WHERE $this->person_communicationTable.idPerson = '$idContact' AND
					  $this->person_communicationTable.idCommunicationType = '$idCommunicationType'
				";
            mysqli_query($con, $query) or die(mysqli_error($con));
        }


        if ($phone != NULL) {
            $query = "SELECT id as idCommunicationType FROM $this->communication_typeTable WHERE name LIKE 'phone'";
            $res = mysqli_query($con, $query);
            $idCommunicationType = "";
            while ($row = mysqli_fetch_array($res)) {
                $idCommunicationType = $row['idCommunicationType'];
                break;
            }
            $query = $idContact == "default"
                ? "INSERT INTO $this->person_communicationTable VALUES(NULL,'$idPerson','$idCommunicationType','$phone')"
                : "UPDATE $this->person_communicationTable SET $this->person_communicationTable.communicationValue = '$phone'
				WHERE $this->person_communicationTable.idPerson = '$idContact' AND
					  $this->person_communicationTable.idCommunicationType = '$idCommunicationType'
				";
            mysqli_query($con, $query) or die(mysqli_error($con));
        }
        if ($skype != NULL) {
            $query = "SELECT id as idCommunicationType FROM $this->communication_typeTable WHERE name LIKE 'skype'";
            $res = mysqli_query($con, $query);
            $idCommunicationType = "";
            while ($row = mysqli_fetch_array($res)) {
                $idCommunicationType = $row['idCommunicationType'];
                break;
            }
            $query = $idContact == "default"
                ? "INSERT INTO $this->person_communicationTable VALUES(NULL,'$idPerson','$idCommunicationType','$skype')"
                : "UPDATE $this->person_communicationTable SET $this->person_communicationTable.communicationValue = '$skype'
				WHERE $this->person_communicationTable.idPerson = '$idContact' AND
					  $this->person_communicationTable.idCommunicationType = '$idCommunicationType'
				";
            mysqli_query($con, $query) or die(mysqli_error($con));
        }

        mysqli_query($con, "COMMIT");
        mysqli_close($con);

        return $idContact == "default" ? $idContactNew : $idContact;
    }

    public function CreateAddressByCompany($idCompany, $companyRegion, $companyCommunity, $companyEmail, $companyPhone, $companySkype, $companyName)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");
        mysqli_query($con, "SET AUTOCOMMIT=0");
        mysqli_query($con, "START TRANSACTION");

        $query = "UPDATE $this->companyTable SET $this->companyTable.region = '$companyRegion' WHERE $this->companyTable.id = '$idCompany'";
        mysqli_query($con, $query) or die(mysqli_error($con));

        $query = "UPDATE $this->companyTable SET $this->companyTable.name = '$companyName' WHERE $this->companyTable.id = '$idCompany'";
        mysqli_query($con, $query) or die(mysqli_error($con));

        $query = "UPDATE $this->companyTable SET $this->companyTable.community = '$companyCommunity' WHERE $this->companyTable.id = '$idCompany'";
        mysqli_query($con, $query) or die(mysqli_error($con));

        $query = "SELECT id as idCommunicationType FROM $this->communication_typeTable WHERE name LIKE 'Email'";
        $res = mysqli_query($con, $query);
        $idCommunicationType = "";
        while ($row = mysqli_fetch_array($res)) {
            $idCommunicationType = $row['idCommunicationType'];
            break;
        }

        $query = "SELECT id FROM
					$this->company_social_networksTable
		WHERE $this->company_social_networksTable.idCompany = $idCompany AND $this->company_social_networksTable.idCommunicationType = $idCommunicationType";
        $res = mysqli_query($con, $query);
        $idExistCommunicationType = "";
        while ($row = mysqli_fetch_array($res)) {
            $idExistCommunicationType = $row['id'];
            break;
        }

        $query = $idExistCommunicationType == ""
            ? "INSERT INTO $this->company_social_networksTable VALUES(NULL,'$idCompany','$idCommunicationType','$companyEmail')"
            : "UPDATE $this->company_social_networksTable SET $this->company_social_networksTable.communicationValue = '$companyEmail' WHERE $this->company_social_networksTable.id='$idExistCommunicationType'";
        mysqli_query($con, $query) or die(mysqli_error($con));

        $query = "SELECT id as idCommunicationType FROM $this->communication_typeTable WHERE name LIKE 'Skype'";
        $res = mysqli_query($con, $query);
        $idCommunicationType = "";
        while ($row = mysqli_fetch_array($res)) {
            $idCommunicationType = $row['idCommunicationType'];
            break;
        }

        $query = "SELECT id FROM
					$this->company_social_networksTable
		WHERE $this->company_social_networksTable.idCompany = $idCompany AND $this->company_social_networksTable.idCommunicationType = $idCommunicationType";
        $res = mysqli_query($con, $query);
        $idExistCommunicationType = "";
        while ($row = mysqli_fetch_array($res)) {
            $idExistCommunicationType = $row['id'];
            break;
        }

        $query = $idExistCommunicationType == ""
            ? "INSERT INTO $this->company_social_networksTable VALUES(NULL,'$idCompany','$idCommunicationType','$companySkype')"
            : "UPDATE $this->company_social_networksTable SET $this->company_social_networksTable.communicationValue = '$companySkype' WHERE $this->company_social_networksTable.id='$idExistCommunicationType'";
        mysqli_query($con, $query) or die(mysqli_error($con));


        $query = "SELECT id as idCommunicationType FROM $this->communication_typeTable WHERE name LIKE 'Phone'";
        $res = mysqli_query($con, $query);
        $idCommunicationType = "";
        while ($row = mysqli_fetch_array($res)) {
            $idCommunicationType = $row['idCommunicationType'];
            break;
        }

        $query = "SELECT id FROM
					$this->company_social_networksTable
		WHERE $this->company_social_networksTable.idCompany = $idCompany AND $this->company_social_networksTable.idCommunicationType = $idCommunicationType";
        $res = mysqli_query($con, $query);
        $idExistCommunicationType = "";
        while ($row = mysqli_fetch_array($res)) {
            $idExistCommunicationType = $row['id'];
            break;
        }

        $query = $idExistCommunicationType == ""
            ? "INSERT INTO $this->company_social_networksTable VALUES(NULL,'$idCompany','$idCommunicationType','$companyPhone')"
            : "UPDATE $this->company_social_networksTable SET $this->company_social_networksTable.communicationValue = '$companyPhone' WHERE $this->company_social_networksTable.id='$idExistCommunicationType'";
        mysqli_query($con, $query) or die(mysqli_error($con));

        mysqli_query($con, "COMMIT");
        mysqli_close($con);
    }

    public function AddSocialNetworksToCompany($idCompany, $ids)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");
        foreach ($ids as $key => $value) {
            $query = "INSERT INTO $this->company_social_networksTable VALUES(NULL,'$idCompany','$value','')";
            mysqli_query($con, $query) or die(mysqli_error($con));
        }

        mysqli_close($con);
    }

    public function SendEmailWithRegistrationLink($emailRegistration)
    {
        //$emailRegistration = "deadean@yandex.ru";
        $to = $emailRegistration;

        $subject = "Press Realise check";
        $message = '
		<html>
			<head>
				<title>Check your registration!</title>
			</head>
			<body>
				<p>Hello</p>
			</body>
		</html>';

        $headers = "Content-type: text/html; charset=windows-1251 \r\n";
        $headers .= "From: MyBrandWire Company <mybrandwire@gmail.com>\r\n";
        //$headers .= "Bcc: birthday-archive@example.com\r\n";

        /*
         $to      = 'deadean@yandex.ru';
         $subject = 'the subject';
         $message = 'hello';
         $headers = 'From: webmaster@example.com' . "\r\n" .
         'Reply-To: webmaster@example.com' . "\r\n" .
         'X-Mailer: PHP/' . phpversion();*/

        return mail($to, $subject, $message, $headers);

    }

    public function SendEmailToUs($email)
    {
        $to = $email->To;

        $subject = $email->Subject;
        $message = '
		<html>
			<head>
				<meta http-equiv="content-type" content="text/html; charset=UTF-8" />
				<title>Check your registration!</title>
			</head>
			<body>' . $email->Message . '
			</body>
		</html>';

        $headers = "Content-type: text/html; charset=UTF-8 \r\n";
        $headers .= "From: <" . $email->From . ">\r\n";

        return mail($to, $subject, $message, $headers);

    }

	public function SaveRecoveryBase(\php\Interfaces\Recovery\CRecoveryBase $item){
		$con = $this->getInstance()->GetConnection();

		$query = "
			SELECT * FROM ".$this->user_recovery."
				WHERE ".$this->user_recovery.".idUser = '$item->idUser'
		";

		$res = mysqli_query($con, $query) or die(mysqli_error($con));
		$isExist = FALSE;
		while ($row = mysqli_fetch_array($res)) {
			$isExist = true;
			break;
		}

		$hash = $item->GetHashCode();
		$query = $isExist
			? "
				UPDATE ".$this->user_recovery." SET "
						.$this->user_recovery.".sendTime =FROM_UNIXTIME($item->sendTime), "
						.$this->user_recovery.".hashCode ='".$hash."'
					WHERE idUser = ".$item->idUser.";"
			: "INSERT INTO ".$this->user_recovery
			." VALUES(
				'$item->idUser',
				FROM_UNIXTIME($item->sendTime),
				'$hash'
				)";
		//echo $query;
		mysqli_query($con, $query) or die(mysqli_error($con));

		$this->getInstance()->CloseConnection($con);
	}

	public function IsInputHashCodeExistAndUseful(\php\Interfaces\Recovery\CRecoveryBase $item){
		$con = $this->getInstance()->GetConnection();

		$hash = $item->GetHashCode();
		$query = "SELECT * FROM ".$this->user_recovery
			." WHERE hashCode ='$item->inputHashCode'";

		//echo $query;

		$res = mysqli_query($con, $query) or die(mysqli_error($con));

		$result = array();
		$isLinkUsed = true;
		while ($row = mysqli_fetch_array($res)) {
			$result['idUser']=$row['idUser'];
			$result['sendTime']=$row['sendTime'];
			$isLinkUsed = false;
			break;
		}
		$result["error"] = strtotime(date('Y-m-d H:i:s', strtotime($result['sendTime'] . ' + 1 day')))>time()
			? \php\enErrors::$NONE : \php\enErrors::$DATE_IS_PASSED;
		$result["error"] = $isLinkUsed ? \php\enErrors::$DATE_IS_PASSED : \php\enErrors::$NONE;

		$this->getInstance()->CloseConnection($con);

		return $result;
	}

    public function IsUserRegistered($login, $password)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $table_ = $this->userstable;
        //$password = md5($password);
        $query = "SELECT * FROM $table_ WHERE login='$login' AND password='$password'";
        //echo $query;

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $user = "";
        while ($row = mysqli_fetch_array($res)) {
            $user = new User();
            $user->id = $row['id'];
            $user->login = $row['login'];
            $user->password = $row['password'];
            $user->companies = $this->GetCompaniesByUser($user->id);
            break;
        }

        return $user;
    }

	public function UpdateUserPassword($idUser, $newPassword)
	{
		$con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

		$query = "
					UPDATE $this->userstable SET
						$this->userstable.password = '$newPassword'
					WHERE id = '$idUser';
					";

		$res = mysqli_query($con, $query);
		if ($res === FALSE) {
			die(mysqli_error($con));
		}

		$query = "SELECT * FROM $this->userstable WHERE id='$idUser'";
		//echo $query;

		$res = mysqli_query($con, $query);

		if ($res === FALSE) {
			die(mysqli_error($con));
		}

		$user = "";
		while ($row = mysqli_fetch_array($res)) {
			$user = new User();
			$user->id = $row['id'];
			$user->login = $row['login'];
			$user->password = $row['password'];
			$user->companies = $this->GetCompaniesByUser($user->id);
			break;
		}

		return $user;
	}

    public function IsLoginRegister($login)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        //$this->PostFillDb();

        $table_ = $this->userstable;

        $query = "SELECT * FROM $table_ WHERE login='$login'";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        while ($row = mysqli_fetch_array($res)) {
            return "1";
        }

        return "0";
    }

	public function IsEmailRegister($email)
	{
		$con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

		$query = "SELECT * FROM $this->userstable WHERE email='$email'";

		$res = mysqli_query($con, $query);

		if ($res === FALSE) {
			die(mysqli_error($con));
		}

		$isExist = "0";
		$idUser = "";
		while ($row = mysqli_fetch_array($res)) {
			$isExist = "1";
			$idUser = $row['id'];
			break;
		}
		$res = array();
		$res['isExist'] = $isExist;
		$res['idUser'] = $idUser;

		return $res;
	}

    public function PublishNews($id)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "
						UPDATE $this->orders SET $this->orders.isPublish = 1 WHERE id = $id
				 ";

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        mysqli_close($con);
    }

    public function RemoveEntity($id, $type)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "";
        $res = "";

		if ($type == csConstants::$csRecoveryBase) {
			$query = "
						DELETE FROM $this->user_recovery WHERE $this->user_recovery.idUser = $id
					";
			$res = mysqli_query($con, $query);
		}

        if ($type == "news") {
            $query = "
						UPDATE $this->pressrealise SET $this->pressrealise.isCompanyOwner = 0 WHERE id = $id
					";
            $res = mysqli_query($con, $query);
        }
        if ($type == "contact") {
            $query = "
						DELETE FROM $this->company_PersonTable WHERE $this->company_PersonTable.idPerson = $id
					";
            $res = mysqli_query($con, $query);
            $query = "
						DELETE FROM $this->personTable WHERE $this->personTable.id = $id
					";
            $res = mysqli_query($con, $query);
        }


        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        mysqli_close($con);
    }

    public function UpdateNews($news)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "
					UPDATE $this->pressrealise SET
						$this->pressrealise.header = '$news->header',
						$this->pressrealise.common = '$news->common',
						$this->pressrealise.content = '$news->content',
						$this->pressrealise.tags = '$news->tags',
						$this->pressrealise.comments = '$news->comments'
					WHERE id = $news->id;
					";
        $res = mysqli_query($con, $query);

        $idOrder = $news->order->id;
        $isPayment = $news->order->payment;

        $query = "
					UPDATE $this->orders SET
						$this->orders.payment = '$isPayment'
					WHERE id = '$idOrder';
					";

        $res = mysqli_query($con, $query);

        //echo $query;

        $query = " DELETE FROM $this->news_categoryTable WHERE $this->news_categoryTable.idNews = '$news->id' ";
        $res = mysqli_query($con, $query);

        foreach ($news->categories as $key => $value) {
            $query = "INSERT INTO $this->news_categoryTable VALUES(NULL,$value->id,$news->id)";
            $res = mysqli_query($con, $query);
        }

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        mysqli_close($con);
    }

    public function UpdateCompany($company)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");
        $query = "
					UPDATE $this->companyTable SET
						$this->companyTable.about = '$company->about'
					WHERE id = '$company->id';
					";
        $res = mysqli_query($con, $query);
        if ($res === FALSE) {
            die(mysqli_error($con));
        }
        mysqli_close($con);

		$this->_company->about = $company->about;
    }

	public function UpdateCompanyLogo($idCompany, $logo){
		$con = $this::GetConnection();
		$query = "
					UPDATE $this->companyTable SET
						$this->companyTable.logo = '$logo'
					WHERE id = '$idCompany';
					";

		//echo $query;
		$res = mysqli_query($con, $query);
		if ($res === FALSE) {
			die(mysqli_error($con));
		}
		$this::CloseConnection($con);

		$this->_company->logo = $logo;
	}

    public function UpdateCompanySocialContacts($idCompany, $name, $type)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "DELETE FROM $this->company_social_networksTable
					WHERE idCompany = $idCompany
						  AND idCommunicationType IN (
              											SELECT id as idCommunicationType FROM $this->communication_typeTable
																							WHERE name LIKE '$type'
													  )";
        echo $query;
        $res = mysqli_query($con, $query);

        $query = "SELECT id as idCommunicationType FROM $this->communication_typeTable WHERE name LIKE '$type'";
        $res = mysqli_query($con, $query);
        $idSocialType = "";
        while ($row = mysqli_fetch_array($res)) {
            $idSocialType = $row['idCommunicationType'];
            break;
        }

        $query = "INSERT INTO $this->company_social_networksTable VALUES(NULL,'$idCompany','$idSocialType','$name')";
        echo $query;
        $res = mysqli_query($con, $query);
        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        mysqli_close($con);
    }

    public function FindNews($param)
    {
        $con = mysqli_connect($this->hostname, $this->username, $this->password, $this->dbName) OR DIE("Не могу создать соединение ");

        $query = "
			SELECT * FROM $this->pressrealise
				WHERE
					($this->pressrealise.status = 'moderated' OR $this->pressrealise.status = 'editPublishedNews')
					AND
					UPPER($this->pressrealise.header) LIKE UPPER('%$param%')
		";

        //echo $query;

        $res = mysqli_query($con, $query);

        if ($res === FALSE) {
            die(mysqli_error($con));
        }

        $result = array();
        while ($row = mysqli_fetch_array($res)) {
            $item = new News();
            $id = $row['id'];
            $item = $this->GetNewsById($id);
            $item->company = $this->GetCompaniesByNews($item->id);
            $result[$item->id] = $item;

            $result[$item->id] = $item;
        }

        mysqli_close($con);
        return $result;
    }

}

;

class Base
{
    protected $model;
    public $ClassType;

    public function __construct()
    {
        $this->model = new Model();
    }

    public function ClearParam($param, $con)
    {
        return mysqli_real_escape_string($con, $param);
    }

    public function ClearParamWithoutConnection($param)
    {
        return mysqli_real_escape_string($this->model->GetConnection(), $param);
    }
}

interface IEntity
{
    public function Save();
}

include_once 'entity.php';
?>

