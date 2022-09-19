app.directive('ngFiles', ['$parse', function ($parse) {

    function fn_link(scope, element, attrs) {
        var onChange = $parse(attrs.ngFiles);
        element.on('change', function (event) {
            onChange(scope, { $files: event.target.files });
        });
    };

    return {
        link: fn_link
    }
}])


app.controller('studentformCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Users";

    $scope.cardTitle = "Student Registration";

    $scope.image_name = "";

    $scope.userTypeList = [
        {
            id: "1",
            name: "Admin"
        },
        {
            id: "2",
            name: "Account"
        },
        {
            id: "1",
            name: "Employee"
        }
    ];

    $scope.userList = [
        {
            id: "1",
            name: "Dhruv",
            email: "dhruv@gmail.com",
            mobile: "0000000000",
            status: true
        },
        {
            id: "2",
            name: "Anish",
            email: "anish@gmail.com",
            mobile: "1111111111",
            status: true
        },
        {
            id: "3",
            name: "Keval",
            email: "keval@gmail.com",
            mobile: "1111100000",
            status: false
        },
    ];

    $scope.userTableParams = new NgTableParams({
    }, {
        dataset: $scope.userList
    });

    $scope.formdata_photo = new FormData();
    $scope.getPhoto = function ($files) {
        angular.forEach($files, function (value, key) {
            $scope.image_name = value.name;
            $scope.formdata_photo.append(key, value);
        });
    };

    $scope.getTempOccupation = function () {

        var data = new Object();

        $http({
            method: 'GET',
            url: 'api/GenOccupation/TempOccupationGet',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.tempTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.getTempOccupation();

    $scope.send = function () {
        //alert("Occupation");
        $http({
            method: 'GET',
            url: 'api/GenOccupation/GenOccupationGet',
            data: $scope.occupation,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.List = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.EnableDisableFile = function () {
        var chkYes = document.getElementById("checkbox2");
        var imageFile = document.getElementById("file1");
        imageFile.disabled = chkYes.checked ? false : true;
        if (!imageFile.disabled) {
            imageFile.focus();
        }
    };   


    $scope.Add = function () {
        alert("Adding");

        var request_photo = {
            method: 'POST',
            url: 'api/GenOccupation/UploadPhoto',
            data: $scope.formdata_photo,
            headers: {
                'Content-Type': undefined
            }
        };
        $http(request_photo).success(function (d) { alert(d); }).error(function () { });

        // Upload Photo end
        $scope.temp.FilePath = $scope.image_name;

        if ($scope.temp.IsSelfEmp === null || $scope.temp.IsSelfEmp === undefined) { } else {

            $http({
                method: 'POST',
                url: 'api/GenOccupation/TempOccupationAdd',
                data: $scope.temp,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.getTempOccupation();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };   

    $scope.modifyTempData = function (data) {
        alert(data.IsEbc);
     
        if (data.IsSelfEmp ==1) {
          
            data.IsSelfEmp = true;
        } else {
            data.IsSelfEmp = false;
        } 
        if (data.IsEbc == 1) {
            //alert("in");
            data.IsEbc = true;
        } else {
            data.IsEbc = false;
        } 

        
        $scope.temp = data;                
    }; 

    $scope.Update = function () {
        alert($scope.temp.IsEbc);

        $http({
            method: 'POST',
            url: 'api/GenOccupation/TempOccupationEdit',
            data: $scope.temp,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getTempOccupation();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.sendSocial = function () {
        alert("Social category");
        $http({
            method: 'GET',
            url: 'api/MstSocialCategory/MstSocialCategoryGet',
            data: $scope.SocialCategoryName,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.List1 = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };     

    //$scope.AddSocial = function () {
    //    alert("Adding social");

    //    $http({
    //        method: 'POST',
    //        url: 'api/MstSocialCategory/TempSocialAdd',
    //        data: $scope.SocialCat,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {

    //            $rootScope.showLoading = false;
    //            // alert("success");
    //            if (response.response_code != "200") {
    //                $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //            }

    //            else {
    //                alert(response.obj);
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //};

});