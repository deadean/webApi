var ViewModel = function(data) {
    this.errorRecoverPassLength = ko.observable(data.errorRecoverPassLength);
    this.errorNewUserEmailIsAlreadyRegistered = ko.observable(data.errorNewUserEmailIsAlreadyRegistered);
    this.newUserEmail = ko.observable(data.newUserEmail);
    this.pass1 = ko.observable(data.pass1);
};

var instanceViewModel = new ViewModel(data);
ko.applyBindings(instanceViewModel);

instanceViewModel.pass1.subscribe(function (newValue) {
    //console.log(newValue);
    instanceViewModel.errorRecoverPassLength(instanceViewModel.pass1().length<=6);
});

instanceViewModel.newUserEmail.subscribe(function (newValue) {
    //console.log(newValue);
    data.newUserEmail = newValue;
    SetUnivaersalJSONAjax3('../ajaxPages/crud.php','checkIsEmailRegister',data
    ,function(data)
    {
        var result = JSON.parse(data)==1;
        instanceViewModel.errorNewUserEmailIsAlreadyRegistered(result);
    });
});
