app.controller('BOSSubjectMapCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage BOS-Subject Map";

    /*Reset BOSSubjectMap*/
    $scope.resetBOSSubjectMap = function () {
        $scope.BOSSub = {};
    };

    /*Get BOSSubjectMap List*/
    $scope.getBOSSubjectMap = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstBoardOfStudySubjectMap/BoardOfStudySubjectMapGet',            
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.BOSSubjectMapTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };    

    /*Get BOS List*/
    $scope.getBOSList = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstBoardOfStudy/MstBoardOfStudyGetForDropDown',
            //data: $scope.BOSSub,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.BOSList = {};
                }
                else {
                    $scope.BOSList = response.obj;
                }

            })
            .error(function (res) {
               
            });
    };

    /*Get Subject List*/
    $scope.getSubjectList = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstSubject/MstSubjectGetForDropDown',
            //data: $scope.BOSSub,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.SubjectList = {};
                }
                else {
                    $scope.SubjectList = response.obj;
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Add BOSSubjectMap*/
    $scope.addBOSSubjectMap = function () {
         
            $http({
                method: 'POST',
                url: 'api/MstBoardOfStudySubjectMap/BoardOfStudySubjectMapAdd',
                data: $scope.BOSSub,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.BOSSub = {};
                        $scope.getBOSSubjectMap();
                        
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        
    };

    /*Modify BOSSubjectMap Data*/
    $scope.modifyBOSSubjectMapData = function (data) {
          
        $scope.showFormFlag = true;
        $scope.BOSSub = data;
        $scope.FacultyGet();
        $scope.MstInstituteGet();       
        $(window).scrollTop(0); 
        };  

    /*Update BOSSubjectMap*/
    $scope.editBOSSubjectMap = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstBoardOfStudySubjectMap/BoardOfStudySubjectMapUpdate',
            data: $scope.BOSSub,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj); 
                    $scope.showFormFlag = false;
                    $scope.getBOSSubjectMap();
                
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Delete BOSSubjectMap*/
    $scope.deleteBOSSubjectMap = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.BOSSub = data;

            $http({
                method: 'POST',
                url: 'api/MstBoardOfStudySubjectMap/BoardOfStudySubjectMapDelete',
                data: $scope.BOSSub,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj); 
                        $scope.getBOSSubjectMap();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };    

    /*Add New BOSSubjectMap*/
    $scope.newBOSSubjectMapAdd = function () {
        $state.go('BOSSubjectMapAdd');
    };

    /*Back to Edit Page of BOSSubjectMap*/
    $scope.backToList = function () {
        $state.go('BOSSubjectMapEdit');
    };

    /*Active Enable BOSSubjectMap*/
    $scope.ShowBOSSubjectMap = function (data) {

        $scope.BOSSub = data;

        $http({
            method: 'POST',
            url: 'api/MstBoardOfStudySubjectMap/BoardofStudySubjectMapIsActive',
            data: $scope.BOSSub,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj); 
                    $scope.getBOSSubjectMap();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable BOSSubjectMap*/
    $scope.HideBOSSubjectMap = function (data) {

        $scope.BOSSub = data;

        $http({
            method: 'POST',
            url: 'api/MstBoardOfStudySubjectMap/BoardofStudySubjectMapIsSuspended',
            data: $scope.BOSSub,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj); 
                    $scope.getBOSSubjectMap();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});