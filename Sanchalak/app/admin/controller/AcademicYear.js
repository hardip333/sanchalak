app.controller('AcademicYearCtrl', function ($scope, $http, $filter, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Academic Year Details";

    /*Reset Academic Year Level*/
    $scope.resetAcademicYear = function () {
        $scope.AcadYear = {};
    };

    /*Get Academic Year List*/
    $scope.getAcademicYear = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/IncAcademicYear/AcademicYearGet',            
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

                    $scope.AcademicYearTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj

                    });
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add Academic Year*/
    $scope.addAcademicYear = function () {

        if ($scope.AcadYear.AcademicYearCode === null || $scope.AcadYear.AcademicYearCode === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/IncAcademicYear/AcademicYearAdd',
                data: $scope.AcadYear,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        alert(response.obj);

                    }
                    else {
                        alert(response.obj); 
                        $scope.AcadYear = {};
                        $scope.getAcademicYear();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Update Academic Year*/
    $scope.editAcademicYear = function () {

        if ($scope.AcadYear.AcademicYearCode === null || $scope.AcadYear.AcademicYearCode === undefined) {

            alert("please check all fields !!!");
        }
        else {
            
            $http({
                method: 'POST',
                url: 'api/IncAcademicYear/AcademicYearUpdate',
                data: $scope.AcadYear,
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
                        $scope.getAcademicYear();
                        $scope.ShowFormFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete Academic Year*/
    $scope.deleteAcademicYear = function (ev,data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.AcadYear = data;
                
                $http({
                    method: 'POST',
                    url: 'api/IncAcademicYear/AcademicYearDelete',
                    data: $scope.AcadYear,
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
                            $scope.getAcademicYear();
                        }
                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });
           
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

    /*Modify Academic Year Data*/
    $scope.modifyAcademicYearData = function (data) {
       
        $scope.ShowFormFlag = true;
        
        var dob = data.FromDate;        
        var ProperDate1 = dob.slice(6);        
        var ProperDate2 = ProperDate1.slice(0, ProperDate1.length - 2);         
        var ProperDate3 = parseInt(ProperDate2);        
        var item = ProperDate3;
        var FinalDate = new Date(item);  
        //alert(FinalDate);
        $scope.secondDate = $filter('date')(FinalDate, "yyyy-MM-dd"); 
        //alert($scope.secondDate);


        var dob1 = data.ToDate;
        var ProperDate4 = dob1.slice(6);
        var ProperDate5 = ProperDate4.slice(0, ProperDate4.length - 2);
        var ProperDate6 = parseInt(ProperDate5);
        var item1 = ProperDate6;
        var FinalDate1 = new Date(item1);
        $scope.secondDate1 = $filter('date')(FinalDate1, "yyyy-MM-dd");         

        $scope.AcadYear = data;
        $(window).scrollTop(0);
    };

    /*Display Academic Year Data*/
    $scope.displayAcademicYear = function (data) {        
        $scope.AcadYear = data;
    };

    /*Add New Academic Year*/
    $scope.newAcademicYearAdd = function (data) {
        $state.go('AcademicYearAdd');
    };

    /*Back to Edit Page of Academic Year*/
    $scope.backToList = function () {
        $state.go('AcademicYearEdit');
    };     

    /*Active Enable Academic Year*/
    $scope.ShowAcademicYear = function (data) {

        $scope.AcadYear = data;
       
        $http({
            method: 'POST',
            url: 'api/IncAcademicYear/AcademicYearIsActiveEnable',
            data: $scope.AcadYear,
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
                    $scope.getAcademicYear();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable Academic Year*/
    $scope.HideAcademicYear = function (data) {

        $scope.AcadYear = data;
             
        $http({
            method: 'POST',
            url: 'api/IncAcademicYear/AcademicYearIsActiveDisable',
            data: $scope.AcadYear,
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
                    $scope.getAcademicYear();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };       

});

        