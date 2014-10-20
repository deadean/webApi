/**
 * Created by dean on 30.06.14.
 */
function resultFunc(data){
    //console.log(data);
    //var objectData = JSON.parse( data );
    //instanceViewModel.about(objectData.about);
}

data={
    IsRememberMe:false,
    userEmail:"",
    errorRecoverEmailMessageState:false
}

var ViewModel = function(data) {
    this.IsRememberMe = ko.observable(data.IsRememberMe);
    this.userEmail = ko.observable(data.userEmail);
    this.errorRecoverEmailMessageState = ko.observable(data.errorRecoverEmailMessageState);
    this.SetRememberMe = function(data) {
        return true;
    }
    this.OpenEmailRecoveryForm = function(data) {
        modules.modals.openModal($('.modal-trigger'),'modal-8ctrEmailRecoveryForm');
    }

    this.TrySendEmail = function(data) {
        data.userEmail =$('#idRecoveryEmail').val();
        SetUnivaersalJSONAjax3('../ajaxPages/crud.php','CheckIsEmailRegister',data
            ,function(data)
            {
                var result = JSON.parse(data)==0;
                instanceViewModel.errorRecoverEmailMessageState(result);
                if(result)
                    modules.modals.openModal($('.modal-trigger'),'modal-8ctrEmailRecoveryForm');
                else
                    modules.modals.openModal($('.modal-trigger'),'modal-8ctrEmailRecoveryHasSent');
            });
    }

    this.data = data;
};

var instanceViewModel = new ViewModel(data);
ko.applyBindings(instanceViewModel);