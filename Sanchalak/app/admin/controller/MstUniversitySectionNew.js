app.controller('MstUniversitySectionNewCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams, $filter) {

    $rootScope.pageTitle = "Manage Mst University Section";
    $scope.UpdateBtnFlag = false;

    /*Reset MstUniversitySection*/
    $scope.resetMstUniversitySection = function () {
        $scope.MstUniversitySection = {};
        $scope.UpdateBtnFlag = false;
    };

    $scope.displayMstUniversitySection = function (data) {
        $scope.MstUniversitySection = data;
    };

    /*Get MstUniversitySection List*/
    $scope.MstUniversitySectionGet = function () {
        
        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstUniversitySectionNew/MstUniversitySectionGet',
            
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
                    $scope.MstUniversitySectionTableParams = new NgTableParams({}, { dataset: response.obj });
                    $scope.MstUniversitySectionData = response.obj;
                    console.log($scope.MstUniversitySectionData);
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add MstUniversitySection Data*/
    $scope.MstUniversitySectionAdd = function () {

        if ($scope.MstUniversitySection.UniversitySectionName == null || $scope.MstUniversitySection.UniversitySectionName == "" || $scope.MstUniversitySection.UniversitySectionName == undefined) 
        {
            alert("Please Add University Section Name..!");
        }
        else if ($scope.MstUniversitySection.UniversitySectionCode == null || $scope.MstUniversitySection.UniversitySectionCode == "" || $scope.MstUniversitySection.UniversitySectionCode == undefined)
        {
            alert("Please Add University Section Code..!");
        }
        else if ($scope.MstUniversitySection.UniversitySectionAddress == null || $scope.MstUniversitySection.UniversitySectionAddress == "" || $scope.MstUniversitySection.UniversitySectionAddress == undefined) {
            alert("Please Add University Section Address..!");
        }
        else if ($scope.MstUniversitySection.CityName == null || $scope.MstUniversitySection.CityName == "" || $scope.MstUniversitySection.CityName == undefined) {
            alert("Please Add City Name..!");
        }
        else if ($scope.MstUniversitySection.Pincode == null || $scope.MstUniversitySection.Pincode == "" || $scope.MstUniversitySection.Pincode == undefined) {
            alert("Please Add Pincode..!");
        }
        else if ($scope.MstUniversitySection.UniversitySectionContactNo == null || $scope.MstUniversitySection.UniversitySectionContactNo == "" || $scope.MstUniversitySection.UniversitySectionContactNo == undefined) {
            alert("Please Add University Section Contact No..!");
        }
        else if ($scope.MstUniversitySection.UniversitySectionFaxNo == null || $scope.MstUniversitySection.UniversitySectionFaxNo == "" || $scope.MstUniversitySection.UniversitySectionFaxNo == undefined) {
            alert("Please Add University Section Fax No..!");
        }
        else if ($scope.MstUniversitySection.UniversitySectionEmail == null || $scope.MstUniversitySection.UniversitySectionEmail == "" || $scope.MstUniversitySection.UniversitySectionEmail == undefined) {
            alert("Please Add University Section Email..!");
        }
        else if ($scope.MstUniversitySection.UniversitySectionUrl == null || $scope.MstUniversitySection.UniversitySectionUrl == "" || $scope.MstUniversitySection.UniversitySectionUrl == undefined) {
            alert("Please Add University Section Url..!");
        }
        else
        {
            $http({
                method: 'POST',
                url: 'api/MstUniversitySectionNew/MstUniversitySectionAdd',
                data: $scope.MstUniversitySection,
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
                        $scope.MstUniversitySectionGet();
                        $scope.MstUniversitySection = {};

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };   

    /*Modify MstUniversitySection Data*/
    $scope.modifyMstUniversitySectionData = function (data) {
        $scope.showFormFlag = true;
        $scope.MstUniversitySection = data;
        $(window).scrollTop(0);
        $scope.UpdateBtnFlag = true;
    };

    /*Update MstUniversitySection Data*/
    $scope.MstUniversitySectionEdit = function () {

        if ($scope.MstUniversitySection.UniversitySectionName == null || $scope.MstUniversitySection.UniversitySectionName == "" || $scope.MstUniversitySection.UniversitySectionName == undefined ) {

            alert("Please Add Designation Name..!");
        }
        else if ($scope.MstUniversitySection.UniversitySectionCode == null || $scope.MstUniversitySection.UniversitySectionCode == "" || $scope.MstUniversitySection.UniversitySectionCode == undefined) {
            alert("Please Add University Section Code..!");
        }
        else if ($scope.MstUniversitySection.UniversitySectionAddress == null || $scope.MstUniversitySection.UniversitySectionAddress == "" || $scope.MstUniversitySection.UniversitySectionAddress == undefined) {
            alert("Please Add University Section Address..!");
        }
        else if ($scope.MstUniversitySection.CityName == null || $scope.MstUniversitySection.CityName == "" || $scope.MstUniversitySection.CityName == undefined) {
            alert("Please Add City Name..!");
        }
        else if ($scope.MstUniversitySection.Pincode == null || $scope.MstUniversitySection.Pincode == "" || $scope.MstUniversitySection.Pincode == undefined) {
            alert("Please Add Pincode..!");
        }
        else if ($scope.MstUniversitySection.UniversitySectionContactNo == null || $scope.MstUniversitySection.UniversitySectionContactNo == "" || $scope.MstUniversitySection.UniversitySectionContactNo == undefined) {
            alert("Please Add University Section Contact No..!");
        }
        else if ($scope.MstUniversitySection.UniversitySectionFaxNo == null || $scope.MstUniversitySection.UniversitySectionFaxNo == "" || $scope.MstUniversitySection.UniversitySectionFaxNo == undefined) {
            alert("Please Add University Section Fax No..!");
        }
        else if ($scope.MstUniversitySection.UniversitySectionEmail == null || $scope.MstUniversitySection.UniversitySectionEmail == "" || $scope.MstUniversitySection.UniversitySectionEmail == undefined) {
            alert("Please Add University Section Email..!");
        }
        else if ($scope.MstUniversitySection.UniversitySectionUrl == null || $scope.MstUniversitySection.UniversitySectionUrl == "" || $scope.MstUniversitySection.UniversitySectionUrl == undefined) {
            alert("Please Add University Section Url..!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstUniversitySectionNew/MstUniversitySectionEdit',
                data: $scope.MstUniversitySection,
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
                        $scope.resetMstUniversitySection();
                        $scope.MstUniversitySectionGet();
                        $scope.MstUniversitySection = {};
                        $scope.showFormFlag = false;
                        $scope.UpdateBtnFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    }; 

    /*Delete MstUniversitySection Data*/
    $scope.MstUniversitySectionDelete = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstUniversitySection = data;

            $http({
                method: 'POST',
                url: 'api/MstUniversitySectionNew/MstUniversitySectionDelete',
                data: $scope.MstUniversitySection,
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
                        $scope.MstUniversitySectionGet();
                        $scope.MstUniversitySection = {};
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            // };
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

});