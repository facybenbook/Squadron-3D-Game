﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class AirplanesRepositoryImpl : IAirplanesRepository {

    private static AirplanesRepositoryImpl INSTANCE;

    private ILocalStorageProvider localStorage;
    private ICahceProvider cache;
    private JsonMapper jsonMapper;

    private AirplanesRepositoryImpl(ILocalStorageProvider localStorage, ICahceProvider cache, JsonMapper jsonMapper) {
        this.localStorage = localStorage;
        this.cache = cache;
        this.jsonMapper = jsonMapper;
    }

    public static AirplanesRepositoryImpl getInstance(ILocalStorageProvider localStorage, ICahceProvider cache, JsonMapper jsonMapper) {
        if (INSTANCE == null)
            INSTANCE = new AirplanesRepositoryImpl(localStorage, cache, jsonMapper);
        return INSTANCE;
    }
    public Task<Airplane> getAirplaneById(int id) {
        return Task.Run(() => {
            string data = localStorage.readFile(ResourcesPath.AIRPLANES_FILE);
            List<Airplane> airplanes = jsonMapper.fromJsonArray<Airplane>(data);
            return airplanes.Find(airplane => airplane.id == id);
        });
    }
}

