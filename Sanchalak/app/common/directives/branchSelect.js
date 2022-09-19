app.directive("branchSelect", function () {
    return {
        templateUrl: "UI/components/branchSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.MstSpecialisationGetByFacultyIdList = [];
            $scope.defaultBranchList = [];
            var branchObj = {
                Id: 0,
                BranchName: "Select Branch"
            }
            $scope.defaultBranchList.push(branchObj);

            $scope.MstSpecialisationGetByFacultyId = function (data) {

                $http({
                    method: 'POST',
                    url: 'api/mstspecialisation/MstSpecialisationGetByFacultyId',
                    data: { FacultyId: data},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                        }
                        else {

                            $scope.tempList = response.obj;
                            $scope.MstSpecialisationGetByFacultyIdList = $scope.defaultBranchList.concat($scope.tempList);

                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }

        }],
        link: function (e, t, a) {
        }
    }
})