/**
 * Created by dean on 09.07.14.
 */

/**
 * Created by dean on 07.06.14.
 */
function resultFunc(data){
    //console.log(data);
//    var objectData = JSON.parse( data );
//    instanceViewModel.about(objectData.about);
}

var ViewModel = function(data) {
    this.idUser = ko.observable(data.idUser);
    this.id = ko.observable(data.id);
    this.errorRecoverPassEqual = ko.observable(data.errorRecoverPassEqual);
    this.errorRecoverPassLength = ko.observable(data.errorRecoverPassLength);
    this.pass1 = ko.observable(data.pass1);
    this.pass2= ko.observable(data.pass2);
    this.IsInputHashCodeExistAndUseful= ko.observable(data.IsInputHashCodeExistAndUseful);
    this.onClickLogin = function(data){
        instanceViewModel.errorRecoverPassEqual(instanceViewModel.pass1()!=instanceViewModel.pass2());
        instanceViewModel.errorRecoverPassLength(instanceViewModel.pass1().length<=6);
        var isError = !instanceViewModel.errorRecoverPassEqual() && !instanceViewModel.errorRecoverPassLength();
        if(isError===false)
            return;

        SetUnivaersalJSONAjax3('../ajaxPages/crud.php','recoverPassword',data,
            function(data){
                if(data==1){
                    window.location="../../company/company.php";
                }
                else{
                    window.location="../../index.php";
                }

        });
    }
};

var instanceViewModel = new ViewModel(data);
ko.applyBindings(instanceViewModel);

instanceViewModel.pass2.subscribe(function (newValue) {
    instanceViewModel.errorRecoverPassEqual(instanceViewModel.pass1()!=newValue);
});

instanceViewModel.pass1.subscribe(function (newValue) {
    //console.log(instanceViewModel.pass1().length>6);
    instanceViewModel.errorRecoverPassLength(instanceViewModel.pass1().length<=6);
});