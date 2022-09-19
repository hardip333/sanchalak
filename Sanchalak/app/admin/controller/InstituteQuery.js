app.controller('InstituteQueryCtrl', function ($scope, $http, $window, $rootScope, $state, $cookies, $localStorage, Upload, $mdDialog, NgTableParams, $localStorage) {

    $rootScope.pageTitle = "Manage Institute Query";

    $scope.InstituteQuery = {};

    var today = new Date();
    var date = today.getDate();
    var month = today.getMonth() + 1;
    var year = today.getFullYear();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();



    $scope.InstituteQueryList = [
        {
            id: '1',
            name: 'Related To Instance Create'
        }, {
            id: '2',
            name: ' Related To Instance PartTerm'
        }, {

            id: '3',
            name: 'Related To Add on Config'
        }, {
            id: '4',
            name: 'Related To Required Document'
        }, {
            id: '5',
            name: 'Related To Application Date Config'
        }, {
            id: '6',
            name: 'Related To Faculty Verfication'
        }, {
            id: '7',
            name: 'Others'
        
        }];

    $scope.clear = function () {
        angular.element("input[type='file']").val(null);
    };

    $scope.resetForm = function () {
        $scope.Query = {};

    }



   

    /*Get Application Payment Report*/
    $scope.getInstituteQuery = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/InstituteQuery/InstituteQueryGet',
            data: data,
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
                    $scope.InstQuery = response.obj;
                    
                }
                
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add Academic Year*/
    $scope.InstituteQueryAdd = function (UserRegistrationId, InstituteId, FacultyId, FacultyEmail, FacultyName, InstituteName, UserName) {
        if ($scope.Query === null || $scope.Query === undefined ||
            $scope.QueryDescription === null || $scope.QueryDescription === undefined) { alert("Enter All Fields"); }
        else {

            //$scope.student.createdById = $rootScope.id;
        //alert("Add Data");
            //debugger;
            var obj = { "UserRegistrationId": UserRegistrationId, "InstituteId": InstituteId, "FacultyId": FacultyId, "Query": $scope.Query, "QueryDescription": $scope.QueryDescription, "UploadImage": $scope.InstituteQueryImageDoc, "FacultyEmail": FacultyEmail, "FacultyName": FacultyName, "InstituteName": InstituteName, "UserName": UserName }
        console.log(obj);
        $scope.InstituteQuery.UploadImage = $scope.InstituteQueryImageDoc;
            $http({
                method: 'POST',
                url: 'api/InstituteQuery/InstituteQueryAdd',
                data: obj,
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
                        $scope.InstituteQuery = {};
                        $scope.getInstituteQuery();

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }
    }

    //Uploading Document Method
    $scope.UploadInstituteQueryImage = function ($files) {
        $scope.SelectedFiles = $files;
        // alert($scope.SelectedFiles[0].name);
        var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop();
        // if ($localStorage.AppObject.AppId) {
        $scope.InstituteQueryImageDoc = "InstituteQueryImageDoc" + "_" + date + "_" + month + "_" + year + "_" + h + '' + m + '' + s + fileExtension;
        $cookies.put("InstituteQueryImageDoc", $scope.InstituteQueryImageDoc);
        //  }



        if ($scope.SelectedFiles && $scope.SelectedFiles.length) {
            Upload.upload({
                url: 'api/InstituteQuery/UploadInstituteQueryDocument',
                data: {
                    files: $scope.SelectedFiles
                }
            }).then(function (response) {
                $scope.Result = response.data;
            }, function (response) {
                if (response.status > 0) {
                    var errorMsg = response.status + ': ' + response.data;
                    alert(errorMsg);
                }
            }, function (evt) {
                var element = angular.element(document.querySelector('#dvProgress'));
                $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
            });
        }
    };


    //Genric Config for file size-Dynamic Validation
    $scope.byId = function (data) {

        return $http({
            method: 'POST',
            url: 'api/StudentRegistration/GenericConfigurationGetById',
            data: { Id: data },
            headers: { "Content-Type": 'application/json' }
        })
    };
    

});

        