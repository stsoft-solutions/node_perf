use actix_web::{App, get, HttpResponse, HttpServer, Responder};
use lazy_static::lazy_static;
use serde::Serialize;

lazy_static! {
    static ref B: Bars  = create_bars();
}

fn create_bars() -> Bars {
    let mut b = vec![];
    for _i in 0..100 {
        b.push(Bar {
            open: 1.5,
            high: 2.5,
            low: 0.7,
            close: 1.9,
        });
    }
    Bars { bars: b }
}

#[get("/bars")]
async fn bars() -> impl Responder {
    HttpResponse::Ok().json(&*B)
}



#[actix_web::main] // or #[tokio::main]
async fn main() -> std::io::Result<()> {
    HttpServer::new(|| {
        App::new().service(bars)
    })
        .bind(("localhost", 5000))?
        .run()
        .await
}

// Struct with contain a one bar
#[derive(Serialize)]
struct Bar {
    open: f64,
    high: f64,
    low: f64,
    close: f64,
}

// Struct with contain a list of bars
#[derive(Serialize)]
struct Bars {
    bars: Vec<Bar>,
}