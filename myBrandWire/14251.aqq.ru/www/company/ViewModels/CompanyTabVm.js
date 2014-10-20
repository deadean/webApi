/**
 * Created by dean on 07.06.14.
 */
function resultFunc(data){
    //console.log(data);
    var objectData = JSON.parse( data );
    instanceViewModel.about(objectData.about);
}

var ViewModel = function(data) {
    this.about = ko.observable(data.about);
    this.isEdit = ko.observable(data.isEdit);
    this.aboutCount = ko.observable(data.aboutCount);
    this.SetEdit = function(data) {
        this.isEdit(true);
        this.aboutCount(this.about().length);
    }
    this.Save = function(data1) {
        if(this.aboutCount()<150){
            modules.modals.openModal($('.modal-trigger'),'modal-8companyAbout');
            return;
        }
        this.isEdit(false);
        ChangeVisibleStatus('idBtnEditCompanyAbout');
        ChangeVisibleStatus('idBtnSaveCompanyAbout');
        $('.'.concat('name-help')).slideUp(500);
        $('.'.concat('name-helpMain')).slideDown(500);
        ChangeSlideDownAnimation('idInputCompanyAbout','name-help');
        ChangeSlideDownAnimation('idCompanyAbout','name-help');
        data.about = this.about();
        SetUnivaersalJSONAjax3('../ajaxPages/crud.php','UpdateInstance',data,resultFunc);
    }
};

//console.log(data);
var instanceViewModel = new ViewModel(data);
ko.applyBindings(instanceViewModel);

instanceViewModel.about.subscribe(function (newValue) {
    //console.log(newValue.length);
    instanceViewModel.aboutCount(newValue.length);
});
