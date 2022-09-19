app.directive("programmepartSelect", function () {
    return {
        templateUrl: "UI/components/programmepartSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.MstProgrammePartGetList = [];
            $scope.defaultPartList = [];
            var PartObj = {
                Id: 0,
                PartName: "Select programme part"
            }
            $scope.defaultPartList.push(PartObj);

            $scope.MstProgrammePartGet = function () {

                $http({
                    method: 'GET',
                    url: 'api/mstprogrammepart/MstProgrammePartGet',
                    data: {},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                        }
                        else {

                            $scope.tempList = response.obj;
                            $scope.MstProgrammePartGetList = $scope.defaultPartList.concat($scope.tempList);

                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            $scope.MstProgrammePartGet();


        }],
        link: function (e, t, a) {
        }
    }
})